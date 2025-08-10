// Original script from Klime
// v240802 - [Khjin] Added null guard and clean-up, updated logic
using Sandbox.Common.ObjectBuilders;
using Sandbox.Game;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using System.Collections.Generic;
using System.Linq;
using VRage.Game.Components;
using VRage.Game.Entity;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRage.ObjectBuilders;

namespace VanillaEx
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_SmallMissileLauncher), false,
        "KJN_KWP_BMB_an_m30A1_100lb_block",
        "KJN_KWP_BMB_an_m57A1_250lb_block",
        "KJN_KWP_BMB_an_m64A1_500lb_block",
        "KJN_KWP_BMB_an_m65A1_1000lb_block",
        "KJN_KWP_BMB_an_m65A1_1000lb_4x_rack_block",
        "KJN_KWP_BMB_an_m65A1_1000lb_4x_rack_mirrored",
        "KJN_KWP_BMB_an_m66A1_2000lb_block",
        "KJN_KWP_BMB_pc1000_1000kg_block",
        "KJN_KWP_BMB_pc1600_1600kg_block",
        "KJN_KWP_ROC_m8_block",
        "KJN_KWP_ROC_hvar_block",
        "KJN_KWP_ROC_tinytim_block",
        "KJN_KWP_SEW_ASW_Hedgehog_block"
    )]
    public class SubpartAmmo : MyGameLogicComponent
    {
        IMySmallMissileLauncher launcher;
        IMyMissileGunObject launcherObj; //Required for CanShoot
        List<MyEntitySubpart> subparts = new List<MyEntitySubpart>();
        Queue<MyEntitySubpart> unfired = new Queue<MyEntitySubpart>();

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            launcher = Entity as IMySmallMissileLauncher;
            NeedsUpdate |= MyEntityUpdateEnum.BEFORE_NEXT_FRAME;
        }

        public override void UpdateOnceBeforeFrame()
        {
            if (launcher.CubeGrid.Physics != null)
            {
                if (launcher.Components.Has<MyHierarchyComponentBase>())
                {
                    var comp = launcher.Components.Get<MyHierarchyComponentBase>();
                    HashSet<IMyEntity> result = new HashSet<IMyEntity>();
                    comp.GetChildrenRecursive(result);
                    foreach (var child in result)
                    {
                        MyEntitySubpart subpart = child as MyEntitySubpart;
                        if(subpart != null)
                        {
                            subparts.Add(subpart);
                        }
                    }
                    // subparts = subparts.OrderBy(sp => sp.DisplayName).ToList();
                    foreach(var subpart in subparts)
                    {
                        unfired.Enqueue(subpart);
                    }
                }
                launcherObj = launcher as IMyMissileGunObject;
                NeedsUpdate |= MyEntityUpdateEnum.EACH_10TH_FRAME;
            }
        }

        public override void UpdateAfterSimulation10()
        {
            MyGunStatusEnum status;
            launcherObj.CanShoot(MyShootActionEnum.PrimaryAction, 0, out status);
            if (status == MyGunStatusEnum.Reloading
            ||  status == MyGunStatusEnum.OutOfAmmo)
            {
                if(unfired.Count > 0)
                {
                    var subpart = unfired.Dequeue();
                    if (subpart != null)
                    { subpart.Render.Visible = false; }
                    else
                    { subparts.Remove(subpart); }
                }
            }
            else
            {
                if(unfired.Count == 0)
                {
                    foreach (var subpart in subparts)
                    {
                        unfired.Enqueue(subpart);
                        subpart.Render.Visible = true;
                    }
                }
            }
        }

        public override void Close()
        {

        }

    }

}