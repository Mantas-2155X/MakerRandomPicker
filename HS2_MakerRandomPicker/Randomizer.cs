using System;
using System.Linq;
using AIChara;
using KKAPI.Maker;
using UnityEngine;

namespace HS2_MakerRandomPicker
{
    public static class Randomizer
    {
        public static Template template;
        
        private static readonly System.Random random = new System.Random();

        public enum RandomizerType
        {
            BODY,
            FACE,
        }

        public static void RandomizeSliders(ChaControl chara, float deviation, RandomizerType type)
        {
            var _template = GetTemplate();
            
            if (type == RandomizerType.BODY)
            {
                var body = chara.fileCustom.body;
                
                for (var i = 0; i < _template.shapeValueBody.Length; i++)
                {
                    var v = RandomFloatDeviation(_template.shapeValueBody[i], deviation);
                    v = Mathf.Clamp(v, -1, 2);

                    body.shapeValueBody[i] = v;
                }
                
                body.areolaSize = Mathf.Clamp(RandomFloatDeviation(_template.areolaSize, deviation), -1, 2);
                body.bustSoftness = Mathf.Clamp(RandomFloatDeviation(_template.bustSoftness, deviation), -1, 2);
                body.bustWeight = Mathf.Clamp(RandomFloatDeviation(_template.bustWeight, deviation), -1, 2);
                body.detailPower = Mathf.Clamp(RandomFloatDeviation(_template.detailPower, deviation), -1, 2);
                body.skinGlossPower = Mathf.Clamp(RandomFloatDeviation(_template.skinGlossPower, deviation / 2), -1, 2);
                body.skinMetallicPower = Mathf.Clamp(RandomFloatDeviation(_template.skinMetallicPower, deviation / 2), -1, 2);
                body.nailGlossPower = Mathf.Clamp(RandomFloatDeviation(_template.nailGlossPower, deviation), -1, 2);
                body.nipGlossPower = Mathf.Clamp(RandomFloatDeviation(_template.nipGlossPower, deviation), -1, 2);
            }
            else if (type == RandomizerType.FACE)
            {
                var face = chara.fileCustom.face;
                
                for (var i = 0; i < _template.shapeValueFace.Length; i++)
                {
                    var v = RandomFloatDeviation(_template.shapeValueFace[i], deviation);
                    v = Mathf.Clamp(v, -1, 2);

                    face.shapeValueFace[i] = v;
                }
                
                face.detailPower = Mathf.Clamp(RandomFloatDeviation(_template.faceDetailPower, deviation / 2), -1, 2);
                face.eyebrowTilt = Mathf.Clamp(RandomFloatDeviation(_template.eyebrowTilt, deviation), -1, 2);
                face.hlTilt = Mathf.Clamp(RandomFloatDeviation(_template.hlTilt, deviation), -1, 2);
                face.makeup.eyeshadowGloss = Mathf.Clamp(RandomFloatDeviation(_template.eyeshadowGloss, deviation), -1, 2);
                face.makeup.lipGloss = Mathf.Clamp(RandomFloatDeviation(_template.lipGloss, deviation), -1, 2);
                face.makeup.cheekGloss = Mathf.Clamp(RandomFloatDeviation(_template.cheekGloss, deviation), -1, 2);
            }
        }

        public static void RandomizeHair(ChaControl chara, bool randomHair, bool randomHairMesh, bool randomHairColor)
        {
            var hair = chara.fileCustom.hair;
            
            if (randomHair)
            {
                var back = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.so_hair_b);
                hair.parts[0].id = back.Keys.ElementAt(random.Next(back.Keys.Count));
            
                var bangs = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.so_hair_f);
                hair.parts[1].id = bangs.Keys.ElementAt(random.Next(bangs.Keys.Count));
                
                if (RandomBool(10))
                {
                    hair.parts[2].id = 0;
                }
                else
                {
                    var side = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.so_hair_s);
                    hair.parts[2].id = side.Keys.ElementAt(random.Next(side.Keys.Count));
                }

                if (RandomBool(10))
                {
                    hair.parts[3].id = 0;
                }
                else
                {
                    var extension = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.so_hair_o);
                    hair.parts[3].id = extension.Keys.ElementAt(random.Next(extension.Keys.Count));
                }
            }

            if (randomHairMesh)
            {
                var meshes = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_hairmeshptn);
                var meshId = meshes.Keys.ElementAt(random.Next(meshes.Keys.Count));
                
                var meshColor = RandomColor();

                for (var i = 0; i < 4; i++)
                {
                    hair.parts[i].meshType = meshId;
                    hair.parts[i].meshColor = meshColor;
                }
            }
            
            if (randomHairColor)
            {
                var baseColor = RandomColor();
                
                Color.RGBToHSV(baseColor, out var h, out var s, out var v);
                
                var startColor = Color.HSVToRGB(h, s, Mathf.Max(v - 0.3f, 0f));
                var endColor = Color.HSVToRGB(h, s, Mathf.Min(v + 0.15f, 1f));
                
                for (var i = 0; i < 4; i++)
                {
                    hair.parts[i].baseColor = baseColor;
                    hair.parts[i].topColor = startColor;
                    hair.parts[i].underColor = endColor;
                    
                    foreach (var colorInfo in hair.parts[i].acsColorInfo)
                        colorInfo.color = RandomColor();
                }
                
                chara.fileCustom.face.eyebrowColor = hair.parts[0].baseColor;
                chara.fileCustom.body.underhairColor = hair.parts[0].baseColor;
            }
        }
        
        public static void RandomizeBody(ChaControl chara, int skinColor)
        {
            var body = chara.fileCustom.body;
            
            var skins = chara.lstCtrl.GetCategoryInfo(chara.sex == 0 ? ChaListDefine.CategoryNo.mt_skin_b : ChaListDefine.CategoryNo.ft_skin_b);
            body.skinId = skins.Keys.ElementAt(random.Next(skins.Keys.Count));
            
            var details = chara.lstCtrl.GetCategoryInfo(chara.sex == 0 ? ChaListDefine.CategoryNo.mt_detail_b : ChaListDefine.CategoryNo.ft_detail_b);
            body.detailId = details.Keys.ElementAt(random.Next(details.Keys.Count));

            var sunburns = chara.lstCtrl.GetCategoryInfo(chara.sex == 0 ? ChaListDefine.CategoryNo.mt_sunburn : ChaListDefine.CategoryNo.ft_sunburn);
            body.sunburnId = sunburns.Keys.ElementAt(random.Next(sunburns.Keys.Count));
            
            var nipples = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_nip);
            body.nipId = nipples.Keys.ElementAt(random.Next(nipples.Keys.Count));
            
            var underhairs = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_underhair);
            body.underhairId = underhairs.Keys.ElementAt(random.Next(underhairs.Keys.Count));
            
            if (skinColor == 2)
                return;
            
            var h = 0f;
            var s = 0f;
            var v = 0f;
            
            switch (skinColor)
            {
                case 0:
                    h = 0.058f;
                    s = RandomFloat(0.16, 0.26);
                    v = RandomFloat(0.68, 0.83);                    
                    break;
                case 1:
                    h = 0.058f;
                    s = RandomFloat(0.19, 0.23);
                    v = RandomFloat(0.56, 0.67);
                    break;
            }

            body.skinColor = Color.HSVToRGB(h, s, v);
            body.sunburnColor = Color.HSVToRGB(h, Mathf.Max(0f, s - 0.1f), v);
            body.nipColor = Color.HSVToRGB(h, Mathf.Min(1f, s + 0.1f), v);
            body.underhairColor = chara.fileCustom.hair.parts[0].baseColor;
        }

        public static void RandomizeFace(ChaControl chara, bool randomFace, bool randomFaceEyes)
        {
            var face = chara.fileCustom.face;

            if (randomFace)
            {
                if (chara.sex == 0)
                {
                    var beards = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_beard);
                    face.beardId = beards.Keys.ElementAt(random.Next(beards.Keys.Count));
                }

                var details = chara.lstCtrl.GetCategoryInfo(chara.sex == 0 ? ChaListDefine.CategoryNo.mt_detail_f : ChaListDefine.CategoryNo.ft_detail_f);
                face.detailId = details.Keys.ElementAt(random.Next(details.Keys.Count));
                
                var eyebrows = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_eyebrow);
                face.eyebrowId = eyebrows.Keys.ElementAt(random.Next(eyebrows.Keys.Count));
                
                var cheeks = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_cheek);
                face.makeup.cheekId = cheeks.Keys.ElementAt(random.Next(cheeks.Keys.Count));
                
                var eyeshadows = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_eyeshadow);
                face.makeup.eyeshadowId = eyeshadows.Keys.ElementAt(random.Next(eyeshadows.Keys.Count));
                
                var lips = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_lip);
                face.makeup.lipId = lips.Keys.ElementAt(random.Next(lips.Keys.Count));
            }

            if (randomFaceEyes)
            {
                var eyelashes = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_eyelash);
                face.eyelashesId = eyelashes.Keys.ElementAt(random.Next(eyelashes.Keys.Count));
                
                var highlight = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_eye_hl);
                face.hlId = highlight.Keys.ElementAt(random.Next(highlight.Keys.Count));

                var pupils = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_eye);
                var pupilId = pupils.Keys.ElementAt(random.Next(pupils.Keys.Count));
                
                var blacks = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_eyeblack);
                var blackId = blacks.Keys.ElementAt(random.Next(blacks.Keys.Count));

                var irisColor = RandomColor();
                
                for (var i = 0; i < face.pupil.Length; i++)
                {
                    var pupil = face.pupil[i];

                    pupil.pupilId = pupilId;
                    pupil.blackId = blackId;

                    pupil.pupilColor = irisColor;
                }
            }
        }
        
        private static double RandomDouble(double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private static float RandomFloat(double minimum, double maximum)
        {
            return (float)RandomDouble(minimum, maximum);
        }

        private static float RandomFloat()
        {
            return (float)random.NextDouble();
        }
        
        private static Color RandomColor()
        {
            return new Color(RandomFloat(), RandomFloat(), RandomFloat());
        }

        private static bool RandomBool(int percentChance = 50)
        {
            return random.Next(100) < percentChance;
        }

        private static float RandomFloatDeviation(float mean, float deviation)
        {
            var x1 = 1 - random.NextDouble();
            var x2 = 1 - random.NextDouble();
            var y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);
            
            return (float)(y1 * deviation + mean);
        }

        private static Template GetTemplate()
        {
            if (template == null)
                AssignTemplate(MakerAPI.GetCharacterControl());

            return template;
        }
        
        public static void AssignTemplate(ChaControl chara)
        {
            template = new Template
            {
                shapeValueBody = new float[chara.fileCustom.body.shapeValueBody.Length],
                shapeValueFace = new float[chara.fileCustom.face.shapeValueFace.Length],
                
                areolaSize = chara.fileCustom.body.areolaSize,
                bustSoftness = chara.fileCustom.body.bustSoftness,
                bustWeight = chara.fileCustom.body.bustWeight,
                detailPower = chara.fileCustom.body.detailPower,
                skinGlossPower = chara.fileCustom.body.skinGlossPower,
                skinMetallicPower = chara.fileCustom.body.skinMetallicPower,
                nailGlossPower = chara.fileCustom.body.nailGlossPower,
                nipGlossPower = chara.fileCustom.body.nipGlossPower,
                
                faceDetailPower = chara.fileCustom.face.detailPower,
                eyebrowTilt = chara.fileCustom.face.eyebrowTilt,
                hlTilt = chara.fileCustom.face.hlTilt,
                eyeshadowGloss = chara.fileCustom.face.makeup.eyeshadowGloss,
                lipGloss = chara.fileCustom.face.makeup.lipGloss,
                cheekGloss = chara.fileCustom.face.makeup.cheekGloss,
            };

            for (var i = 0; i < template.shapeValueBody.Length; i++)
                template.shapeValueBody[i] = chara.fileCustom.body.shapeValueBody[i];
            
            for (var i = 0; i < template.shapeValueFace.Length; i++)
                template.shapeValueFace[i] = chara.fileCustom.face.shapeValueFace[i];
        }
        
        public class Template
        {
            public float[] shapeValueBody;
            public float[] shapeValueFace;

            public float areolaSize;
            public float bustSoftness;
            public float bustWeight;
            public float detailPower;
            public float skinGlossPower;
            public float skinMetallicPower;
            public float nailGlossPower;
            public float nipGlossPower;

            public float faceDetailPower;
            public float eyebrowTilt;
            public float hlTilt;
            public float eyeshadowGloss;
            public float lipGloss;
            public float cheekGloss;
        }
    }
}