using Sandbox.Definitions;
using Sandbox.Game;
using Sandbox.Game.Entities;
using Sandbox.Game.World;
using Sandbox.ModAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.Entity;
using VRage.Game.ModAPI;
using VRage.ObjectBuilders;
using VRage.Utils;
using VRageMath;

namespace VanillaEx
{
    [MySessionComponentDescriptor(MyUpdateOrder.AfterSimulation)]
    public class AmmoMechanics : MySessionComponentBase
    {
        public void InitDefinitions()
        {
            // ===============
            //  Register Ammo
            // ===============
            //
            // HE Bombs
            Register(KJN_KWP_BMB_an_m30A1_100lb_ammo);
            Register(KJN_KWP_BMB_an_m57A1_250lb_ammo);
            Register(KJN_KWP_BMB_an_m64A1_500lb_ammo);
            Register(KJN_KWP_BMB_an_m65A1_1000lb_ammo);
            Register(KJN_KWP_BMB_an_m66A1_2000lb_ammo);
            // AP Bombs
            Register(KJN_KWP_BMB_pc1000_1000kg_ammo);
            Register(KJN_KWP_BMB_pc1600_1600kg_ammo);
            // Rockets
            Register(KJN_KWP_ROC_m8_ammo);
            Register(KJN_KWP_ROC_hvar_ammo);
            Register(KJN_KWP_ROC_tinytim_ammo);
            // Flak
            Register(KJN_KWP_GUN_flak36_hevt_ammo);
            Register(KJN_KWP_GUN_flak39_hevt_ammo);
            Register(KJN_KWP_GUN_flak40_hevt_ammo);
            // Hedgehog
            Register(KJN_KWP_SEW_ASW_Hedgehog_ammo);

            // ==================
            //  Register Weapons
            // ==================
            //
            // HE Bombs
            Register(KJN_KWP_BMB_an_m30A1_100lb_block);
            Register(KJN_KWP_BMB_an_m57A1_250lb_block);
            Register(KJN_KWP_BMB_an_m64A1_500lb_block);
            Register(KJN_KWP_BMB_an_m65A1_1000lb_block);
            Register(KJN_KWP_BMB_an_m66A1_2000lb_block);
            Register(KJN_KWP_BMB_an_m65A1_1000lb_4x_rack_block);
            Register(KJN_KWP_BMB_an_m65A1_1000lb_4x_rack_mirrored);
            // AP Bombs
            Register(KJN_KWP_BMB_pc1000_1000kg_block);
            Register(KJN_KWP_BMB_pc1600_1600kg_block);
            // Rockets
            Register(KJN_KWP_ROC_m8_block);
            Register(KJN_KWP_ROC_hvar_block);
            Register(KJN_KWP_ROC_tinytim_block);
            // Flak
            Register(KJN_KWP_GUN_flak36_block);
            Register(KJN_KWP_GUN_flak39_block);
            Register(KJN_KWP_GUN_flak40_block);
        }

        #region Ammo Definitions
        private AmmoDef KJN_KWP_BMB_an_m30A1_100lb_ammo = new AmmoDef()
        {
            SubtypeId = "KJN_KWP_BMB_an_m30A1_100lb_ammo",
            Fuse = new FuseDefinition()
            {
                MinTicksToArm = 30,
                DetectRange = 0,
            },
        };
        private AmmoDef KJN_KWP_BMB_an_m57A1_250lb_ammo = new AmmoDef()
        {
            SubtypeId = "KJN_KWP_BMB_an_m57A1_250lb_ammo",
            Fuse = new FuseDefinition()
            {
                MinTicksToArm = 30,
                DetectRange = 0,
            },
        };
        private AmmoDef KJN_KWP_BMB_an_m64A1_500lb_ammo = new AmmoDef()
        {
            SubtypeId = "KJN_KWP_BMB_an_m64A1_500lb_ammo",
            Fuse = new FuseDefinition()
            {
                MinTicksToArm = 30,
                DetectRange = 0,
            },
        };
        private AmmoDef KJN_KWP_BMB_an_m65A1_1000lb_ammo = new AmmoDef()
        {
            SubtypeId = "KJN_KWP_BMB_an_m65A1_1000lb_ammo",
            Fuse = new FuseDefinition()
            {
                MinTicksToArm = 30,
                DetectRange = 0,
            },
        };
        private AmmoDef KJN_KWP_BMB_an_m66A1_2000lb_ammo = new AmmoDef()
        {
            SubtypeId = "KJN_KWP_BMB_an_m66A1_2000lb_ammo",
            Fuse = new FuseDefinition()
            {
                MinTicksToArm = 30,
                DetectRange = 0,
            },
        };
        private AmmoDef KJN_KWP_BMB_pc1000_1000kg_ammo = new AmmoDef()
        {
            SubtypeId = "KJN_KWP_BMB_pc1000_1000kg_ammo",
            Fuse = new FuseDefinition()
            {
                MinTicksToArm = 30,
                DetectRange = 0,
            },
        };
        private AmmoDef KJN_KWP_BMB_pc1600_1600kg_ammo = new AmmoDef()
        {
            SubtypeId = "KJN_KWP_BMB_pc1600_1600kg_ammo",
            Fuse = new FuseDefinition()
            {
                MinTicksToArm = 30,
                DetectRange = 0,
            },
        };
        private AmmoDef KJN_KWP_ROC_m8_ammo = new AmmoDef()
        {
            SubtypeId = "KJN_KWP_ROC_m8_ammo",
            Fuse = new FuseDefinition()
            {
                MinTicksToArm = 5,
                DetectRange = 0,
            },
        };
        private AmmoDef KJN_KWP_ROC_hvar_ammo = new AmmoDef()
        {
            SubtypeId = "KJN_KWP_ROC_hvar_ammo",
            Fuse = new FuseDefinition()
            {
                MinTicksToArm = 30,
                DetectRange = 1.5f,
            }
        };
        private AmmoDef KJN_KWP_ROC_tinytim_ammo = new AmmoDef()
        {
            SubtypeId = "KJN_KWP_ROC_tinytim_ammo",
            Fuse = new FuseDefinition()
            {
                MinTicksToArm = 5,
                DetectRange = 0,
            }
        };
        private AmmoDef KJN_KWP_GUN_flak36_hevt_ammo = new AmmoDef()
        {
            SubtypeId = "KJN_KWP_GUN_flak36_hevt_ammo",
            Fuse = new FuseDefinition()
            {
                MinTicksToArm = 5,
                DetectRange = 5,
            }
        };
        private AmmoDef KJN_KWP_GUN_flak39_hevt_ammo = new AmmoDef()
        {
            SubtypeId = "KJN_KWP_GUN_flak39_hevt_ammo",
            Fuse = new FuseDefinition()
            {
                MinTicksToArm = 5,
                DetectRange = 7,
            }
        };
        private AmmoDef KJN_KWP_GUN_flak40_hevt_ammo = new AmmoDef()
        {
            SubtypeId = "KJN_KWP_GUN_flak40_hevt_ammo",
            Fuse = new FuseDefinition()
            {
                MinTicksToArm = 5,
                DetectRange = 10,
            }
        };
        private AmmoDef KJN_KWP_SEW_ASW_Hedgehog_ammo = new AmmoDef()
        {
            SubtypeId = "KJN_KWP_SEW_ASW_Hedgehog_ammo",
            Fuse = new FuseDefinition()
            {
                MinTicksToArm = 5,
                DetectRange = 2.5f,
            }
        };
        #endregion

        #region Weapon Definitions
        private WeaponDef KJN_KWP_BMB_an_m30A1_100lb_block = new WeaponDef()
        {
            SubtypeId = "KJN_KWP_BMB_an_m30A1_100lb_block",
            AmmoOffsets = new Vector3D(0, 0, -0.15),
            InitialVelocity = new Vector3D(0, 0, -2),
        };
        private WeaponDef KJN_KWP_BMB_an_m57A1_250lb_block = new WeaponDef()
        {
            SubtypeId = "KJN_KWP_BMB_an_m57A1_250lb_block",
            AmmoOffsets = new Vector3D(0, 0, -0.25),
            InitialVelocity = new Vector3D(0, 0, -2),
        };
        private WeaponDef KJN_KWP_BMB_an_m64A1_500lb_block = new WeaponDef()
        {
            SubtypeId = "KJN_KWP_BMB_an_m64A1_500lb_block",
            AmmoOffsets = new Vector3D(0, 0, -0.35),
            InitialVelocity = new Vector3D(0, 0, -2),
        };
        private WeaponDef KJN_KWP_BMB_an_m65A1_1000lb_block = new WeaponDef()
        {
            SubtypeId = "KJN_KWP_BMB_an_m65A1_1000lb_block",
            AmmoOffsets = new Vector3D(0, 0, -0.60),
            InitialVelocity = new Vector3D(0, 0, -2),
        };
        
        // For custom mirror blocks, they must end with either block or mirrored
        private WeaponDef KJN_KWP_BMB_an_m65A1_1000lb_4x_rack_block = new WeaponDef()
        {
            SubtypeId = "KJN_KWP_BMB_an_m65A1_1000lb_4x_rack_block",
            AmmoOffsets = new Vector3D(0, 0, -1.25),
            InitialVelocity = new Vector3D(0, 0, -4),
        };
        private WeaponDef KJN_KWP_BMB_an_m65A1_1000lb_4x_rack_mirrored = new WeaponDef()
        {
            SubtypeId = "KJN_KWP_BMB_an_m65A1_1000lb_4x_rack_mirrored",
            AmmoOffsets = new Vector3D(0, 0, -1.25),
            InitialVelocity = new Vector3D(0, 0, -4),
        };
        
        private WeaponDef KJN_KWP_BMB_an_m66A1_2000lb_block = new WeaponDef()
        {
            SubtypeId = "KJN_KWP_BMB_an_m66A1_2000lb_block",
            AmmoOffsets = new Vector3D(0, 0, -0.75),
            InitialVelocity = new Vector3D(0, 0, -2),
        };
        private WeaponDef KJN_KWP_BMB_pc1000_1000kg_block = new WeaponDef()
        {
            SubtypeId = "KJN_KWP_BMB_pc1000_1000kg_block",
            AmmoOffsets = new Vector3D(0, 0, -0.60),
            InitialVelocity = new Vector3D(0, 0, -2),
        };
        private WeaponDef KJN_KWP_BMB_pc1600_1600kg_block = new WeaponDef()
        {
            SubtypeId = "KJN_KWP_BMB_pc1600_1600kg_block",
            AmmoOffsets = new Vector3D(0, 0, -0.75),
            InitialVelocity = new Vector3D(0, 0, -2),
        };
        private WeaponDef KJN_KWP_ROC_m8_block = new WeaponDef()
        {
            SubtypeId = "KJN_KWP_ROC_m8_block",
            AmmoOffsets = new Vector3D(0, 0, -0.25),
        };
        private WeaponDef KJN_KWP_ROC_hvar_block = new WeaponDef()
        {
            SubtypeId = "KJN_KWP_ROC_hvar_block",
            AmmoOffsets = new Vector3D(0, -5, -0.15),
        };
        private WeaponDef KJN_KWP_ROC_tinytim_block = new WeaponDef()
        {
            SubtypeId = "KJN_KWP_ROC_tinytim_block",
            AmmoOffsets = new Vector3D(0, 0, -0.25),
        };
        private WeaponDef KJN_KWP_GUN_flak36_block = new WeaponDef()
        {
            SubtypeId = "KJN_KWP_GUN_flak36_block",
            AmmoOffsets = new Vector3D(0, 0, -0.75),
        };
        private WeaponDef KJN_KWP_GUN_flak39_block = new WeaponDef()
        {
            SubtypeId = "KJN_KWP_GUN_flak39_block",
            AmmoOffsets = new Vector3D(0, 0, -0.75),
        };
        private WeaponDef KJN_KWP_GUN_flak40_block = new WeaponDef()
        {
            SubtypeId = "KJN_KWP_GUN_flak40_block",
            AmmoOffsets = new Vector3D(0, 0, -0.75),
        };
        #endregion

        #region Ammo Mechanics
        private Dictionary<string, AmmoDef> ammoDefs = new Dictionary<string, AmmoDef>();
        private Dictionary<string, WeaponDef> weaponDefs = new Dictionary<string, WeaponDef>();
        private Dictionary<string, MyWeaponDefinition> sbcWeaponDefs = new Dictionary<string, MyWeaponDefinition>();
        private Dictionary<string, MyAmmoDefinition> sbcAmmoDefs = new Dictionary<string, MyAmmoDefinition>();
        private Dictionary<long, MissileEntity> missiles = new Dictionary<long, MissileEntity>();
        private Queue<MissileEntity> explosionQueue = new Queue<MissileEntity>();

        #region Base Functions
        public override void Init(MyObjectBuilder_SessionComponent sessionComponent)
        {
            InitDefinitions();
            InitDefinitionsSBC();
        }

        private void InitDefinitionsSBC()
        {
            // Weapons
            foreach (string weaponSubtypeId in weaponDefs.Keys)
            {
                if (!sbcWeaponDefs.ContainsKey(weaponSubtypeId))
                {
                    MyWeaponDefinition sbcWeapon;
                    var id = MyDefinitionId.Parse($"MyObjectBuilder_WeaponDefinition/{weaponSubtypeId}");
                    if (MyDefinitionManager.Static.TryGetWeaponDefinition(id, out sbcWeapon))
                    { sbcWeaponDefs.Add(weaponSubtypeId, sbcWeapon); }
                    else
                    {
                        id = MyDefinitionId.Parse($"MyObjectBuilder_WeaponDefinition/{weaponSubtypeId.Replace("mirrored", "block")}");
                        if (MyDefinitionManager.Static.TryGetWeaponDefinition(id, out sbcWeapon))
                        { sbcWeaponDefs.Add(weaponSubtypeId, sbcWeapon); }
                    }
                }
            }

            foreach (var weapon in sbcWeaponDefs.Values)
            {
                foreach (var magazine in weapon.AmmoMagazinesId)
                {
                    var id = MyDefinitionId.Parse($"MyObjectBuilder_MissileAmmoDefinition/{KJN_KWP_BMB_an_m30A1_100lb_ammo}");

                    MyAmmoMagazineDefinition mag = MyDefinitionManager.Static.GetAmmoMagazineDefinition(magazine);
                    MyAmmoDefinition sbcAmmo = MyDefinitionManager.Static.GetAmmoDefinition(mag.AmmoDefinitionId);
                    string magId = mag.AmmoDefinitionId.SubtypeName;
                    if (sbcAmmo != null && !sbcAmmoDefs.ContainsKey(magId))
                    { sbcAmmoDefs.Add(magId, sbcAmmo); }
                }
            }
        }

        public override void BeforeStart()
        {
            if (MyAPIGateway.Session.IsServer)
            {
                MyAPIGateway.Missiles.OnMissileAdded += MissileAdded;
                MyAPIGateway.Missiles.OnMissileMoved += MissileMoved;
                MyAPIGateway.Missiles.OnMissileCollided += MissileCollided;
                MyAPIGateway.Missiles.OnMissileRemoved += MissileRemoved;
                InitDefinitionsSBC();
            }
        }

        protected override void UnloadData()
        {
            if (MyAPIGateway.Session.IsServer)
            {
                MyAPIGateway.Missiles.OnMissileAdded -= MissileAdded;
                MyAPIGateway.Missiles.OnMissileMoved -= MissileMoved;
                MyAPIGateway.Missiles.OnMissileCollided -= MissileCollided;
                MyAPIGateway.Missiles.OnMissileRemoved -= MissileRemoved;
            }
        }

        private void MissileAdded(IMyMissile obj)
        {
            string missileId = obj.AmmoDefinition.Id.SubtypeName;
            try
            {
                if (IsSupportedMissile(missileId))
                {
                    if (!missiles.ContainsKey(obj.EntityId))
                    {
                        // Get the reference to the launcher
                        MyEntity entity = MyEntities.GetEntityById(obj.LauncherId);
                        IMySmallMissileLauncher launcher = entity as IMySmallMissileLauncher;

                        // Create and register missile
                        MissileEntity missileEntity = new MissileEntity();
                        missileEntity.Me = obj;
                        missileEntity.SubtypeId = missileId;
                        missileEntity.Launcher = launcher;
                        missileEntity.OwnerId = launcher.OwnerId;
                        missileEntity.LauncherSubtypeId = launcher.BlockDefinition.SubtypeId;
                        missileEntity.Faction = TryGetBlockFaction(launcher);
                        missileEntity.AmmoDef = ammoDefs[missileId];
                        missileEntity.CurrArmingTicks = missileEntity.AmmoDef.Fuse.MinTicksToArm;
                        missileEntity.AmmoDefSBC = sbcAmmoDefs[missileEntity.SubtypeId];
                        missileEntity.WeaponDefSBC = sbcWeaponDefs[missileEntity.LauncherSubtypeId];
                        missileEntity.OwningEntity = MyEntities.GetEntityById(launcher.EntityId);   // Rifle or block
                        missileEntity.OwningEntityAbsolute = GetPlayerEntity(launcher.OwnerId);     // Character
                        missileEntity.Weapon = missileEntity.OwningEntity;                          // Shooter entity (rifle, block)
                        missiles.Add(obj.EntityId, missileEntity);
                        RepositionAmmo(missileEntity);
                    }
                    else
                    {
                        ChatMessage($"Existing missile: {obj.EntityId}");
                    }
                }
                else
                {
                    ChatMessage($"Unsupported Missile: {missileId}");
                }
            }
            catch (Exception ex) 
            {
                ChatMessage($"Error: {ex.Message}");
            }
        }

        private void MissileRemoved(IMyMissile obj)
        {
            if (missiles.ContainsKey(obj.EntityId))
            {
                missiles.Remove(obj.EntityId);
            }
        }

        private void MissileMoved(IMyMissile obj, ref Vector3 Velocity)
        {
            if (missiles.ContainsKey(obj.EntityId))
            {
                CheckProximityFuse(obj.EntityId);
            }
        }

        private void MissileCollided(IMyMissile obj)
        {
            if (missiles.ContainsKey(obj.EntityId))
            {
                explosionQueue.Enqueue(missiles[obj.EntityId]);
            }
        }

        public override void UpdateAfterSimulation()
        {
            if (MyAPIGateway.Session.IsServer)
            {
                UpdateArmingTicks();
                SpawnExplosions();
            }
        }

        public void UpdateArmingTicks()
        {
            foreach (var missile in missiles.Values)
            {
                missile.CurrArmingTicks--;
            }
        }
        
        public MyEntity GetPlayerEntity(long ownerId)
        {
            List<IMyPlayer> players = new List<IMyPlayer>();
            MyAPIGateway.Players.GetPlayers(players);
            foreach(var player in players)
            {
                if(player.IdentityId == ownerId)
                {
                    return (MyEntity)player.Character;
                }
            }
            return null;
        }

        #endregion

        #region Extra Mechanics

        public void SpawnExplosions()
        {
            while (explosionQueue.Count > 0)
            {
                
                MissileEntity missile = explosionQueue.Dequeue();
                MyMissileAmmoDefinition missileDef = missile.AmmoDefSBC as MyMissileAmmoDefinition;
                float damage = missileDef.MissileExplosionDamage * missileDef.ExplosiveDamageMultiplier;

                BoundingSphereD blastArea = new BoundingSphereD(missile.Me.GetPosition(), missileDef.MissileExplosionRadius);
                MyExplosionInfo blastInfo = new MyExplosionInfo(0,
                                                                damage,
                                                                blastArea,
                                                                MyExplosionTypeEnum.CUSTOM,
                                                                true);
                blastInfo.CustomEffect = missileDef.EndOfLifeEffect;
                blastInfo.CustomSound = missileDef.EndOfLifeSound;
                blastInfo.KeepAffectedBlocks = true;
                MyExplosions.AddExplosion(ref blastInfo);
                missile.Me.Destroy();
            }
        }

        public void RepositionAmmo(MissileEntity missile)
        {
            if (weaponDefs.ContainsKey(missile.LauncherSubtypeId))
            {
                // Reset to position of launcher
                WeaponDef weaponDef = weaponDefs[missile.LauncherSubtypeId];
                Vector3D origVel = missile.Me.LinearVelocity;

                // Launcher position and axes (already normalized)
                MatrixD wm = missile.Launcher.WorldMatrix;

                // Calculate new position in one step
                Vector3D newPos = missile.Launcher.GetPosition()
                       + wm.Forward * weaponDef.AmmoOffsets.Y
                       + wm.Up * weaponDef.AmmoOffsets.Z
                       + wm.Left * weaponDef.AmmoOffsets.X;

                Vector3D launchBoost =
                       wm.Forward * weaponDef.InitialVelocity.Y   // forward speed
                       + wm.Up * weaponDef.InitialVelocity.Z      // upward speed
                       + wm.Left * weaponDef.InitialVelocity.X;   // sideways speed

                // Apply new position and restore velocity
                missile.Me.SetPosition(newPos);
                missile.Me.LinearVelocity = origVel + launchBoost;
            }
        }

        public void CheckProximityFuse(long entityId)
        {
            MissileEntity missileEntity = missiles[entityId];
            if (missileEntity.AmmoDef.Fuse.MinTicksToArm > 0
            && missileEntity.CurrArmingTicks > 0)
            { return; }

            if (ProximityFuseCheck(missileEntity))
            {
                explosionQueue.Enqueue(missileEntity);
            }
        }

        public bool ProximityFuseCheck(MissileEntity missile, bool enemiesOnly = true)
        {
            // Always return false for ammo without fuse
            FuseDefinition fuse = missile.AmmoDef.Fuse;
            if (fuse.DetectRange == 0)
            { return false; }

            // Get entities within the fuse range
            List<MyEntity> entitiesInSpehere = new List<MyEntity>();
            BoundingSphereD fuseArea = new BoundingSphereD(missile.Me.GetPosition(), fuse.DetectRange);
            MyGamePruningStructure.GetAllEntitiesInSphere(ref fuseArea, entitiesInSpehere);

            // Check each entity (must be grids) that can trigger the fuse
            foreach (MyEntity entity in entitiesInSpehere)
            {
                IMyCubeGrid grid = entity as IMyCubeGrid;
                if (grid != null)
                {
                    if (enemiesOnly)
                    {
                        long gridOwner = grid.BigOwners.Count > 0 ? grid.BigOwners[0] : -1;
                        IMyFaction gridFaction = MyAPIGateway.Session.Factions
                            .TryGetPlayerFaction(gridOwner);
                        if (gridFaction != null && missile.Faction != null)
                        {
                            var rel = MyAPIGateway.Session.Factions
                                .GetRelationBetweenFactions(gridFaction.FactionId, missile.Faction.FactionId);
                            if (rel != MyRelationsBetweenFactions.Friends)
                            { return true; }
                            else
                            { return false; }
                        }
                        else
                        { return true; }
                    }
                    else
                    { return true; }
                }
            }
            return false;
        }

        #endregion

        #endregion

        #region Utility
        public bool IsSupportedMissile(string subtypeId)
        {
            return ammoDefs.ContainsKey(subtypeId);
        }

        public bool IsNullOrEmpty(string value)
        {
            return (value == null || value == string.Empty);
        }

        public IMyFaction TryGetBlockFaction(IMySmallMissileLauncher missileLauncher)
        {
            return MyAPIGateway.Session.Factions.TryGetFactionByTag(missileLauncher.GetOwnerFactionTag());
        }

        public void ChatMessage(string message)
        {
            if (MyAPIGateway.Session.IsServer)
            {
                MyVisualScriptLogicProvider.SendChatMessage($"VanillaEx: {message}");
            }
            else
            {
                MyAPIGateway.Utilities.ShowNotification($"VanillaEx: {message}", 5000, "White");
            }
        }
        #endregion
        
        #region Definitions
        public void Register(BaseDef definition)
        {
            if (definition is WeaponDef)
            {
                weaponDefs.Add(definition.SubtypeId, definition as WeaponDef);
            }
            else if (definition is AmmoDef)
            {
                ammoDefs.Add(definition.SubtypeId, definition as AmmoDef);
            }
        }

        public class MissileEntity
        {
            public IMyMissile Me;
            public IMySmallMissileLauncher Launcher;
            public IMyFaction Faction;
            public string SubtypeId;
            public string LauncherSubtypeId;
            public long OwnerId;
            public int CurrArmingTicks;
            public AmmoDef AmmoDef;
            public MyWeaponDefinition WeaponDefSBC;
            public MyAmmoDefinition AmmoDefSBC;
            public MyEntity OwningEntity;
            public MyEntity OwningEntityAbsolute;
            public MyEntity Weapon;
        }

        public class BaseDef
        {
            public string SubtypeId;
        }

        public class AmmoDef : BaseDef
        {
            public FuseDefinition Fuse;
        }

        public class WeaponDef : BaseDef
        {
            public Vector3D AmmoOffsets = Vector3D.Zero;
            public Vector3D InitialVelocity = Vector3D.Zero;
        }

        public struct FuseDefinition
        {
            public int MinTicksToArm;
            public float DetectRange;
        }
        #endregion
    }
}
