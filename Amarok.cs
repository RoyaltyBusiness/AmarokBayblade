using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VehicleFramework;
using VehicleFramework.VehicleParts;
using VehicleFramework.VehicleTypes;
using System.IO;
using System.Reflection;
using UnityEngine.U2D;
using System.Collections;
using UWE;
using static Nautilus.Assets.PrefabTemplates.FabricatorTemplate;
using UnityEngine.Assertions;
using VehicleFramework.Assets;
using VehicleFramework.Engines;
using RoyalCommonalities.Items.Materials;

namespace AmarokBayblade
{
    public class Amarok : Submarine
    {
        public static IEnumerator Register()
        {
            GetAssets();
            Submarine Amarok = assets.model.EnsureComponent<Amarok>();
            Amarok.name = "Amarok";
            yield return CoroutineHost.StartCoroutine(VehicleRegistrar.RegisterVehicle(Amarok));
            yield break;
        }
        public static VehicleFramework.Assets.VehicleAssets assets;
        public static void GetAssets()
        {
            assets = AssetBundleInterface.GetVehicleAssetsFromBundle("assets/amarok", "Amarok_vehicle");
        }
        public override string vehicleDefaultName
        {
            get
            {
                Language main = Language.main;
                bool flag = !(main != null);
                string result;
                if (flag)
                {
                    result = "Amarok";
                }
                else
                {
                    result = main.Get("Amarok");
                }
                return result;
            }
        }
        public override BoxCollider BoundingBoxCollider
        {
            get
            {
                return transform.Find("BoundingBoxCollider").gameObject.GetComponent<BoxCollider>();
            }
        }
        public override List<VehiclePilotSeat> PilotSeats
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehiclePilotSeat>();
                VehicleFramework.VehicleParts.VehiclePilotSeat vps = new VehicleFramework.VehicleParts.VehiclePilotSeat();
                Transform mainSeat = transform.Find("PilotSeat");
                vps.Seat = mainSeat.gameObject;
                vps.SitLocation = mainSeat.Find("SitLocation").gameObject;
                vps.LeftHandLocation = mainSeat;
                vps.RightHandLocation = mainSeat;
                vps.ExitLocation = mainSeat.Find("ExitLocation");
                list.Add(vps);
                return list;
            }
        }

        public override List<VehicleHatchStruct> Hatches
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleHatchStruct>();

                VehicleFramework.VehicleParts.VehicleHatchStruct interior_vhs = new VehicleFramework.VehicleParts.VehicleHatchStruct();
                Transform intHatch = transform.Find("Hatches/InsideHatch");
                interior_vhs.Hatch = intHatch.gameObject;
                interior_vhs.EntryLocation = intHatch.Find("Entry");
                interior_vhs.ExitLocation = intHatch.Find("Exit");
                interior_vhs.SurfaceExitLocation = intHatch.Find("SurfaceExit");

                VehicleFramework.VehicleParts.VehicleHatchStruct exterior_vhs = new VehicleFramework.VehicleParts.VehicleHatchStruct();
                Transform extHatch = transform.Find("Hatches/OutsideHatch");
                exterior_vhs.Hatch = extHatch.gameObject;
                exterior_vhs.EntryLocation = interior_vhs.EntryLocation;
                exterior_vhs.ExitLocation = interior_vhs.ExitLocation;
                exterior_vhs.SurfaceExitLocation = interior_vhs.SurfaceExitLocation;

                list.Add(interior_vhs);
                list.Add(exterior_vhs);
                return list;
            }
        }

        public override GameObject VehicleModel
        {
            get
            {
                return assets.model;
            }
        }

        public override GameObject CollisionModel
        {
            get
            {
                return transform.Find("Collider").gameObject;
            }
        }
        public override List<GameObject> TetherSources
        {
            get
            {
                var list = new List<GameObject>();
                foreach (Transform child in transform.Find("TetherSources"))
                {
                    list.Add(child.gameObject);
                }
                return list;
            }
        }
        public override GameObject StorageRootObject
        {
            get
            {
                return transform.Find("StorageRoot").gameObject;
            }
        }

        public override GameObject ModulesRootObject
        {
            get
            {
                return transform.Find("ModulesRoot").gameObject;
            }
        }
        public override List<GameObject> WaterClipProxies
        {
            get
            {
                var list = new List<GameObject>();
                foreach (Transform child in transform.Find("WaterClipProxies"))
                {
                    list.Add(child.gameObject);
                }
                return list;
            }
        }
        public override List<VehicleUpgrades> Upgrades
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleUpgrades>();
                VehicleFramework.VehicleParts.VehicleUpgrades vu = new VehicleFramework.VehicleParts.VehicleUpgrades();
                vu.Interface = transform.Find("Upgrades").gameObject;
                vu.Flap = vu.Interface;
                list.Add(vu);
                return list;
            }
        }

        public override List<VehicleBattery> Batteries
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleBattery>();

                VehicleFramework.VehicleParts.VehicleBattery vb1 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb1.BatterySlot = transform.Find("Batteries/Battery1").gameObject;
                list.Add(vb1);

                VehicleFramework.VehicleParts.VehicleBattery vb2 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb2.BatterySlot = transform.Find("Batteries/Battery2").gameObject;
                list.Add(vb2);

                VehicleFramework.VehicleParts.VehicleBattery vb3 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb3.BatterySlot = transform.Find("Batteries/Battery3").gameObject;
                list.Add(vb3);

                VehicleFramework.VehicleParts.VehicleBattery vb4 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb4.BatterySlot = transform.Find("Batteries/Battery4").gameObject;
                list.Add(vb4);

                VehicleFramework.VehicleParts.VehicleBattery vb5 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb5.BatterySlot = transform.Find("Batteries/Battery5").gameObject;
                list.Add(vb5);

                VehicleFramework.VehicleParts.VehicleBattery vb6 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb6.BatterySlot = transform.Find("Batteries/Battery6").gameObject;
                list.Add(vb6);
                return list;
            }
        }
        public override List<VehicleStorage> InnateStorages
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleStorage>();

                Transform innate1 = transform.Find("InnateStorages/Storage1");
                Transform innate2 = transform.Find("InnateStorages/Storage2");
                Transform innate3 = transform.Find("InnateStorages/Storage3");
                Transform innate4 = transform.Find("InnateStorages/Storage4");

                VehicleFramework.VehicleParts.VehicleStorage IS1 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS1.Container = innate1.gameObject;
                IS1.Height = 4;
                IS1.Width = 8;
                list.Add(IS1);
                VehicleFramework.VehicleParts.VehicleStorage IS2 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS2.Container = innate2.gameObject;
                IS2.Height = 4;
                IS2.Width = 8;
                list.Add(IS2);
                VehicleFramework.VehicleParts.VehicleStorage IS3 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS3.Container = innate3.gameObject;
                IS3.Height = 4;
                IS3.Width = 8;
                list.Add(IS3);
                VehicleFramework.VehicleParts.VehicleStorage IS4 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS4.Container = innate4.gameObject;
                IS4.Height = 4;
                IS4.Width = 8;
                list.Add(IS4);

                return list;
            }
        }
        public override List<VehicleFloodLight> HeadLights
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleFloodLight>();

                list.Add(new VehicleFramework.VehicleParts.VehicleFloodLight
                {
                    Light = transform.Find("lights_parent/headlights/C").gameObject,
                    Angle = 70,
                    Color = Color.green,
                    Intensity = 1.3f,
                    Range = 90f
                });

                return list;
            }
        }
        public override Dictionary<TechType, int> Recipe
        {

            get
            {
                Dictionary<TechType, int> recipe = new Dictionary<TechType, int>();
                recipe.Add(ChromaticIngot.Info.TechType, 3);
                recipe.Add(EnhancedWiringKit.Info.TechType, 3);
                recipe.Add(LaminatedGlass.Info.TechType, 3);
                recipe.Add(TechType.PrecursorIonCrystal, 1);
                return recipe;

            }
        }
        public override List<Transform> LavaLarvaAttachPoints
        {
            get
            {
                var list = new List<Transform>();
                foreach (Transform child in transform.Find("LavaLarvaAttachPoints"))
                {
                    list.Add(child);
                }
                return list;
            }
        }
        public override ModVehicleEngine Engine
        {
            get
            {
                return gameObject.EnsureComponent<AmarokEngine>();

            }

        }
        public override GameObject LeviathanGrabPoint => transform.Find("GrabPoint").gameObject;
        public override Atlas.Sprite CraftingSprite => VehicleFramework.Assets.SpriteHelper.GetSprite("assets/AmarokCrafter.png");
        public override Atlas.Sprite PingSprite => VehicleFramework.Assets.SpriteHelper.GetSprite("assets/AmarokPing.png");
        public override Sprite EncyclopediaImage => VehicleFramework.Assets.SpriteHelper.GetSpriteRaw("assets/AmarokDatabank.png");
        public override string Description => "Appropriately Large Multifunctional Architect Submarine";
        public override string EncyclopediaEntry => "todo";
        public override int BaseCrushDepth => 600;

        public override int CrushDepthUpgrade1 => 300;

        public override int CrushDepthUpgrade2 => 400;

        public override int CrushDepthUpgrade3 => 3400;

        public override int MaxHealth => 7500;

        public override int Mass => 3600;
        public override int NumModules => 8;
    }
}
