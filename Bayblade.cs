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
    internal class Bayblade : Submersible
    {
        public static IEnumerator Register()
        {
            GetAssets();
            Submersible Bayblade = assets.model.EnsureComponent<Bayblade>();
            Bayblade.name = "Bayblade";
            yield return CoroutineHost.StartCoroutine(VehicleRegistrar.RegisterVehicle(Bayblade));
            yield break;
        }
        public static VehicleFramework.Assets.VehicleAssets assets;
        public static void GetAssets()
        {
            assets = AssetBundleInterface.GetVehicleAssetsFromBundle("assets/bayblade", "bayblade_vehicle");
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
                    result = "Bayblade";
                }
                else
                {
                    result = main.Get("Bayblade");
                }
                return result;
            }
        }
        public override VehiclePilotSeat PilotSeat
        {
            get
            {
                VehicleFramework.VehicleParts.VehiclePilotSeat vps = new VehicleFramework.VehicleParts.VehiclePilotSeat();
                Transform mainSeat = transform.Find("Pilotseat");
                vps.Seat = mainSeat.gameObject;
                vps.SitLocation = mainSeat.Find("SitLocation").gameObject;
                vps.LeftHandLocation = mainSeat;
                vps.RightHandLocation = mainSeat;
                return vps;
            }
        }

        public override List<VehicleHatchStruct> Hatches
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleHatchStruct>();

                foreach (Transform tr in CollisionModel.transform)
                {
                    VehicleFramework.VehicleParts.VehicleHatchStruct thisHatch = new VehicleFramework.VehicleParts.VehicleHatchStruct();
                    thisHatch.Hatch = tr.gameObject;
                    thisHatch.ExitLocation = transform.Find("Hatch/ExitPosition");
                    thisHatch.SurfaceExitLocation = thisHatch.ExitLocation;
                    list.Add(thisHatch);
                }

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

                VehicleFramework.VehicleParts.VehicleStorage IS1 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS1.Container = innate1.gameObject;
                IS1.Height = 9;
                IS1.Width = 7;
                list.Add(IS1);
                VehicleFramework.VehicleParts.VehicleStorage IS2 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS2.Container = innate2.gameObject;
                IS2.Height = 9;
                IS2.Width = 7;
                list.Add(IS2);

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
                    Light = transform.Find("lights_parent/headlights/L").gameObject,
                    Angle = 70,
                    Color = Color.green,
                    Intensity = 1.3f,
                    Range = 90f
                });
                list.Add(new VehicleFramework.VehicleParts.VehicleFloodLight
                {
                    Light = transform.Find("lights_parent/headlights/R").gameObject,
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
                recipe.Add(ChromaticIngot.Info.TechType, 1);
                recipe.Add(EnhancedWiringKit.Info.TechType, 2);
                recipe.Add(LaminatedGlass.Info.TechType, 1);
                recipe.Add(TechType.PrecursorIonCrystal, 1);
                return recipe;

            }
        }
        public override BoxCollider BoundingBoxCollider
        {
            get
            {
                return transform.Find("BoundingBoxCollider").gameObject.GetComponent<BoxCollider>();
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
                return gameObject.EnsureComponent<BaybladeEngine>();

            }

        }
        public override bool HasArms => true;
        public override VehicleArmsProxy Arms
        {
            get
            {
                return new VehicleArmsProxy
                {
                    leftArmPlacement = transform.Find("Arms/ArmLeft"),
                    rightArmPlacement = transform.Find("Arms/ArmRight"),
                    originalLeftArm = transform.Find("OrArms/OrArmLeft").gameObject,
                    originalRightArm = transform.Find("OrArms/OrArmRight").gameObject
                };
            }
        }
        public override GameObject LeviathanGrabPoint => transform.Find("GrabPoint").gameObject;
        public override Atlas.Sprite CraftingSprite => VehicleFramework.Assets.SpriteHelper.GetSprite("assets/BaybladeCrafter.png");
        public override Atlas.Sprite PingSprite => VehicleFramework.Assets.SpriteHelper.GetSprite("assets/BaybladePing.png");
        public override Sprite EncyclopediaImage => VehicleFramework.Assets.SpriteHelper.GetSpriteRaw("assets/BaybladeDatabank.png");
        public override string Description => "Small and Agile Architect submersible";
        public override string EncyclopediaEntry => "todo";
        public override int BaseCrushDepth => 300;

        public override int CrushDepthUpgrade1 => 600;

        public override int CrushDepthUpgrade2 => 400;

        public override int CrushDepthUpgrade3 => 3500;

        public override int MaxHealth => 3000;

        public override int Mass => 1200;
        public override int NumModules => 5;
    }
}
