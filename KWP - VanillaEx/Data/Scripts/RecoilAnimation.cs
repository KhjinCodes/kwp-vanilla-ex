// Original script from Klime
// v241215 - [Khjin] Modified so that the subpart orientation is kept
using Sandbox.Common.ObjectBuilders;
using Sandbox.Game.Weapons;
using Sandbox.ModAPI;
using System;
using System.Collections.Generic;
using VRage.Game.Components;
using VRage.Game.Entity;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRage.ObjectBuilders;
using VRageMath;

namespace VanillaEx
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_SmallMissileLauncher), false,
            "KJN_KWP_GUN_flak36_block",
            "KJN_KWP_GUN_flak39_block",
            "KJN_KWP_GUN_flak40_block",
            "KJN_KWP_GUN_2pdr_OQF_40mm_block"
    )]
    public class RecoilAnimation : MyGameLogicComponent
    {
        #region Recoil Definitions
        private RecoilDef KJN_KWP_GUN_flak36_block = new RecoilDef()
        {
            RecoilLength = 6,
            RetractionLength = 6,
            RecoilMovePerTick = 0.1f,
            RetractionMovePerTick = 0.1f,
            InitOffset = 0.1f,
        };
        private RecoilDef KJN_KWP_GUN_flak39_block = new RecoilDef()
        {
            RecoilLength = 6,
            RetractionLength = 6,
            RecoilMovePerTick = 0.1f,
            RetractionMovePerTick = 0.1f,
            InitOffset = 0.1f,
        };
        private RecoilDef KJN_KWP_GUN_flak40_block = new RecoilDef()
        {
            RecoilLength = 8,
            RetractionLength = 8,
            RecoilMovePerTick = 0.1f,
            RetractionMovePerTick = 0.1f,
            InitOffset = 0.1f,
        };
        private RecoilDef KJN_KWP_GUN_2pdr_OQF_40mm_block = new RecoilDef()
        {
            RecoilLength = 4,
            RetractionLength = 4,
            RecoilMovePerTick = 0.1f,
            RetractionMovePerTick = 0.1f,
            InitOffset = 0.1f,
        };
        #endregion

        private void InitDefinitions()
        {
            Register("KJN_KWP_GUN_flak36_block", KJN_KWP_GUN_flak36_block);
            Register("KJN_KWP_GUN_flak39_block", KJN_KWP_GUN_flak39_block);
            Register("KJN_KWP_GUN_flak40_block", KJN_KWP_GUN_flak40_block);
            Register("KJN_KWP_GUN_2pdr_OQF_40mm_block", KJN_KWP_GUN_2pdr_OQF_40mm_block);
        }

        #region Recoil Mechanics
        public IMySmallMissileLauncher gun; //Change IMySmallMissileLauncher to your type if you aren't using it as a base
        public string barrelName = "barrel"; //Name of the barrel subpart

        public int recoilLength = 8; //Time of the initial recoil (in ticks)
        public int retractionLength = 8; //Time of the barrel retraction (in ticks)

        public float recoilMovePerTick = 0.1f; //Length the barrel moves in recoil, per tick(meters)
        public float retractionMovePerTick = 0.1f; //Length the barrel moves in retraction, per tick(meters)
        //IMPORTANT : To make sure the barrel doesn't teleport, your recoilLength x recoilMovePerTick should be equal to retractionLength x retractionMovePerTick
        //            e.g: 15*0.1 = 30 *0.05

        public float initOffset = 0f;
        public bool offsetApplied = false;

        public bool useCustomTiming = false;
        public long listenID = 3323868922; //Pick a random number here. It must be unique among guns using Whips framework

        //Core stuff below here
        public long prevshot;
        public bool newshot = false;
        public IMyGunObject<MyGunBase> gunUser;
        public MyEntitySubpart Barrel = null;
        public MatrixD LocalMat = MatrixD.Identity;

        public const ushort RecoilNetID = 13924;
        public List<byte> EmptByte = new List<byte>();
        public int currentLength = 0;
        public bool customNewShot = false;

        Dictionary<string, RecoilDef> recoilDefs = new Dictionary<string, RecoilDef>();

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            gun = Entity as IMySmallMissileLauncher;
            NeedsUpdate = MyEntityUpdateEnum.BEFORE_NEXT_FRAME;
            InitDefinitions();

            // Customize the recoil parameters
            RecoilDef recoilDef = recoilDefs[gun.BlockDefinition.SubtypeName];
            recoilLength = recoilDef.RecoilLength;
            retractionLength = recoilDef.RetractionLength;
            recoilMovePerTick = recoilDef.RecoilMovePerTick;
            retractionMovePerTick = recoilDef.RetractionMovePerTick;
            initOffset = recoilDef.InitOffset;
            offsetApplied = false;
        }

        public override void UpdateOnceBeforeFrame()
        {
            if (gun.CubeGrid.Physics != null)
            {
                MyAPIGateway.Multiplayer.RegisterMessageHandler(RecoilNetID, RecoilHandler);
                if (useCustomTiming)
                {
                    MyAPIGateway.Utilities.RegisterMessageHandler(listenID, WhiplashHandle);
                }
                gunUser = gun as IMyGunObject<MyGunBase>;

                gun.TryGetSubpart(barrelName, out Barrel);
                if (Barrel != null)
                {
                    TriggerAnimation();
                }
                NeedsUpdate = MyEntityUpdateEnum.EACH_FRAME;
            }
        }

        public override void UpdateAfterSimulation()
        {
            if (MyAPIGateway.Session.IsServer)
            {
                if (gunUser.GunBase.LastShootTime.Ticks > prevshot && gun.IsShooting)
                {
                    prevshot = gunUser.GunBase.LastShootTime.Ticks;
                    newshot = true;
                    EmptByte.Clear();
                    EmptByte.AddRange(BitConverter.GetBytes(gun.EntityId));
                    MyAPIGateway.Multiplayer.SendMessageToOthers(RecoilNetID, EmptByte.ToArray());
                }
            }
            if (!useCustomTiming)
            {
                if (newshot)
                {
                    if (Barrel != null)
                    {
                        TriggerAnimation();
                    }
                }
            }
            else
            {
                if (customNewShot)
                {
                    TriggerAnimation();
                }
            }
        }

        public void TriggerAnimation()
        {
            LocalMat = Barrel.PositionComp.LocalMatrix;

            // Apply initial offset
            if(!offsetApplied)
            {
                LocalMat.Translation += (LocalMat.Backward * initOffset);
                offsetApplied = true;
            }

            if(currentLength <= recoilLength)
            {
                LocalMat.Translation -= (LocalMat.Backward * recoilMovePerTick);
            }
            else if(currentLength > recoilLength && currentLength <= (recoilLength + retractionLength))
            {
                LocalMat.Translation += (LocalMat.Backward * retractionMovePerTick);
            }
            else
            {
                currentLength = 0;
                newshot = false;
                customNewShot = false;
            }

            Barrel.PositionComp.LocalMatrix = LocalMat;
            currentLength += 1;
        }

        public void RecoilHandler(byte[] obj)
        {
            if (gun != null && gun.EntityId == BitConverter.ToInt64(obj,0))
            {
                newshot = true;
            }
        }

        public void WhiplashHandle(object obj)
        {
            if (useCustomTiming && gun != null)
            {
                if ((long)obj == gun.EntityId)
                {
                    customNewShot = true;
                }
            }
        }

        public override void Close()
        {
            MyAPIGateway.Utilities.UnregisterMessageHandler(listenID, WhiplashHandle);
            MyAPIGateway.Multiplayer.UnregisterMessageHandler(RecoilNetID, RecoilHandler);
        }

        private void Register(string subtypeId, RecoilDef def)
        {
            recoilDefs.Add(subtypeId, def);
        }

        public struct RecoilDef
        {
            public int RecoilLength;
            public int RetractionLength;
            public float RecoilMovePerTick;
            public float RetractionMovePerTick;
            public float InitOffset;
        }

        #endregion
    }
}