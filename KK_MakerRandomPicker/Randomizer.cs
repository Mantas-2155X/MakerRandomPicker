using System;
using System.Linq;
using KKAPI.Maker;
using UnityEngine;

namespace KK_MakerRandomPicker
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
                body.detailPower = Mathf.Clamp(RandomFloatDeviation(_template.bodyDetailPower, deviation), -1, 2);
                body.nailGlossPower = Mathf.Clamp(RandomFloatDeviation(_template.nailGlossPower, deviation), -1, 2);
                body.nipGlossPower = Mathf.Clamp(RandomFloatDeviation(_template.nipGlossPower, deviation), -1, 2);
                body.skinGlossPower = Mathf.Clamp(RandomFloatDeviation(_template.skinGlossPower, deviation), -1, 2);
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
                
                face.cheekGlossPower = Mathf.Clamp(RandomFloatDeviation(_template.cheekGlossPower, deviation), -1, 2);
                face.detailPower = Mathf.Clamp(RandomFloatDeviation(_template.faceDetailPower, deviation), -1, 2);
                face.eyelineUpWeight = Mathf.Clamp(RandomFloatDeviation(_template.eyelineUpWeight, deviation), -1, 2);
                face.hlDownY = Mathf.Clamp(RandomFloatDeviation(_template.hlDownY, deviation), -1, 2);
                face.hlUpY = Mathf.Clamp(RandomFloatDeviation(_template.hlUpY, deviation), -1, 2);
                face.lipGlossPower = Mathf.Clamp(RandomFloatDeviation(_template.lipGlossPower, deviation), -1, 2);
                face.pupilHeight = Mathf.Clamp(RandomFloatDeviation(_template.pupilHeight, deviation), -1, 2);
                face.pupilWidth = Mathf.Clamp(RandomFloatDeviation(_template.pupilWidth, deviation), -1, 2);
            }
        }

        public static void RandomizeHair(ChaControl chara, bool randomHair, bool randomHairColor)
        {
            var hair = chara.fileCustom.hair;
            
            if (randomHair)
            {
                var back = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.bo_hair_b);
                hair.parts[0].id = back.Keys.ElementAt(random.Next(back.Keys.Count));
            
                var bangs = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.bo_hair_f);
                hair.parts[1].id = bangs.Keys.ElementAt(random.Next(bangs.Keys.Count));
                
                var gloss = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_hairgloss);
                hair.glossId = gloss.Keys.ElementAt(random.Next(gloss.Keys.Count));
                
                if (RandomBool(10))
                {
                    hair.parts[2].id = 0;
                }
                else
                {
                    var side = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.bo_hair_s);
                    hair.parts[2].id = side.Keys.ElementAt(random.Next(side.Keys.Count));
                }

                if (RandomBool(10))
                {
                    hair.parts[3].id = 0;
                }
                else
                {
                    var extension = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.bo_hair_o);
                    hair.parts[3].id = extension.Keys.ElementAt(random.Next(extension.Keys.Count));
                }
            }

            if (randomHairColor)
            {
                var baseColor = RandomColor();
                
                Color.RGBToHSV(baseColor, out var h, out var s, out var v);
                
                var startColor = Color.HSVToRGB(h, s, Mathf.Max(v - 0.3f, 0f));
                var endColor = Color.HSVToRGB(h, s, Mathf.Min(v + 0.15f, 1f));
                
                hair.parts[0].baseColor = baseColor;
                hair.parts[0].startColor = startColor;
                hair.parts[0].endColor = endColor;
                hair.parts[1].baseColor = baseColor;
                hair.parts[1].startColor = startColor;
                hair.parts[1].endColor = endColor;
                hair.parts[2].baseColor = baseColor;
                hair.parts[2].startColor = startColor;
                hair.parts[2].endColor = endColor;
                hair.parts[3].baseColor = baseColor;
                hair.parts[3].startColor = startColor;
                hair.parts[3].endColor = endColor;
                
                chara.fileCustom.face.eyebrowColor = hair.parts[0].baseColor;
                chara.fileCustom.body.underhairColor = hair.parts[0].baseColor;
                
                for (var i = 0; i < 4; i++)
                {
                    for (var j = 0; j < 4; j++)
                    {
                        hair.parts[i].acsColor[j] = RandomColor();
                    }
                }
            }
        }
        
        public static void RandomizeBody(ChaControl chara, int skinColor)
        {
            var body = chara.fileCustom.body;

            var details = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_body_detail);
            body.detailId = details.Keys.ElementAt(random.Next(details.Keys.Count));
            body.detailPower = 0.5f;
            
            var sunburns = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_sunburn);
            body.sunburnId = sunburns.Keys.ElementAt(random.Next(sunburns.Keys.Count));
            
            var nipples = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_nip);
            body.nipId = nipples.Keys.ElementAt(random.Next(nipples.Keys.Count));
            
            var underhairs = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_underhair);
            body.underhairId = underhairs.Keys.ElementAt(random.Next(underhairs.Keys.Count));
            
            if (skinColor == 2)
                return;
            
            var h = 0f;
            var s = 0f;
            var v = 0f;
            
            switch (skinColor)
            {
                case 0:
                    h = 0.06f;
                    RandomPointInTriangle(0.02f, 1f, 0.1f, 0.91f, 0.11f, 1f, out s, out v);               
                    break;
                case 1:
                    h = 0.06f;
                    s = RandomFloat(0.13, 0.39);
                    v = RandomFloat(0.66, 0.98);
                    break;
            }

            var subColor = Color.HSVToRGB(h, Mathf.Min(1f, s + 0.1f), Mathf.Max(0f, v - 0.1f));
            subColor.r = Mathf.Min(1f, subColor.r + 0.1f);
            
            body.skinMainColor = Color.HSVToRGB(h, s, v);
            body.skinSubColor = subColor;
            body.skinGlossPower = RandomFloat();
            body.sunburnColor = Color.HSVToRGB(h, Mathf.Max(0f, s - 0.1f), v);
            body.nipColor = Color.HSVToRGB(h, Mathf.Min(1f, s + 0.1f), v);
            body.areolaSize = RandomFloat();
            body.underhairColor = chara.fileCustom.hair.parts[0].baseColor;
            body.nailColor = RandomColor();
            body.nailGlossPower = RandomFloat();
            body.drawAddLine = RandomBool();
        }

        public static void RandomizeFace(ChaControl chara, bool randomFace, bool randomFaceEyes)
        {
            var face = chara.fileCustom.face;

            if (randomFace)
            {
                var heads = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.bo_head);
                face.headId = heads.Keys.ElementAt(random.Next(heads.Keys.Count));
                
                var details = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_face_detail);
                face.detailId = details.Keys.ElementAt(random.Next(details.Keys.Count));
                face.detailPower = RandomFloat();
                face.lipGlossPower = RandomFloat();
                
                var eyebrows = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_eyebrow);
                face.eyebrowId = eyebrows.Keys.ElementAt(random.Next(eyebrows.Keys.Count));
                face.eyebrowColor = chara.fileCustom.hair.parts[0].baseColor;
                
                var noses = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_nose);
                face.noseId = noses.Keys.ElementAt(random.Next(noses.Keys.Count));
                
                face.moleId = 0;
                
                var lips = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_lipline);
                face.lipLineId = lips.Keys.ElementAt(random.Next(lips.Keys.Count));
                face.lipLineColor = chara.fileCustom.body.skinSubColor;

                face.lipGlossPower = RandomFloat();
                face.doubleTooth = RandomBool(5);
            }

            if (randomFaceEyes)
            {
                var hlUps = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_eye_hi_up);
                face.hlUpId = hlUps.Keys.ElementAt(random.Next(hlUps.Keys.Count));
                face.hlUpColor = RandomBool(5) ? RandomColor() : Color.white;
                
                var hlDowns = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_eye_hi_down);
                face.hlDownId = hlDowns.Keys.ElementAt(random.Next(hlDowns.Keys.Count));
                face.hlDownColor = RandomBool(5) ? RandomColor() : Color.white;
                
                var whites = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_eye_white);
                face.whiteId = whites.Keys.ElementAt(random.Next(whites.Keys.Count));
                face.whiteBaseColor = RandomBool(5) ? RandomColor() : Color.white;
                face.whiteSubColor = RandomBool(5) ? RandomColor() : Color.white;
                
                var elUps = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_eyeline_up);
                face.eyelineUpId = elUps.Keys.ElementAt(random.Next(elUps.Keys.Count));
                
                var elDowns = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_eyeline_down);
                face.eyelineDownId = elDowns.Keys.ElementAt(random.Next(elDowns.Keys.Count));
                
                Color.RGBToHSV(face.pupil[0].baseColor, out var h, out var s, out var v);
                v = Mathf.Clamp(v - 0.3f, 0f, 1f);
                face.eyelineColor = Color.HSVToRGB(h, s, v);
                
                var pupils = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_eye);
                var grads = chara.lstCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_eye_gradation);

                for (var i = 0; i < face.pupil.Length; i++)
                {
                    var pupilId = pupils.Keys.ElementAt(random.Next(pupils.Keys.Count));
                    var blackId = grads.Keys.ElementAt(random.Next(grads.Keys.Count));
                    
                    face.pupil[i].id = pupilId;
                    face.pupil[i].baseColor = RandomColor();
                    face.pupil[i].subColor = RandomColor();
                    
                    face.pupil[i].gradMaskId = blackId;
                    face.pupil[i].gradBlend = RandomFloat();
                    face.pupil[i].gradOffsetY = RandomFloat();
                    face.pupil[i].gradScale = RandomFloat();
                }
                
                if (RandomBool(95))
                    face.pupil[1].Copy(face.pupil[0]);
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

        private static void RandomPointInTriangle(float x1, float y1, float x2, float y2, float x3, float y3, out float x, out float y)
        {
            var r1 = (float)random.NextDouble();
            var r2 = (float)random.NextDouble();
            var sqr1 = (float)Math.Sqrt(r1);

            x = (1 - sqr1) * x1 + sqr1 * (1 - r2) * x2 + sqr1 * r2 * x3;
            y = (1 - sqr1) * y1 + sqr1 * (1 - r2) * y2 + sqr1 * r2 * y3;
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
                bodyDetailPower = chara.fileCustom.body.detailPower,
                nailGlossPower = chara.fileCustom.body.nailGlossPower,
                nipGlossPower = chara.fileCustom.body.nipGlossPower,
                skinGlossPower = chara.fileCustom.body.skinGlossPower,
                
                cheekGlossPower = chara.fileCustom.face.cheekGlossPower,
                faceDetailPower = chara.fileCustom.face.detailPower,
                eyelineUpWeight = chara.fileCustom.face.eyelineUpWeight,
                hlDownY = chara.fileCustom.face.hlDownY,
                hlUpY = chara.fileCustom.face.hlUpY,
                lipGlossPower = chara.fileCustom.face.lipGlossPower,
                pupilHeight = chara.fileCustom.face.pupilHeight,
                pupilWidth = chara.fileCustom.face.pupilWidth,
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
            public float bodyDetailPower;
            public float nailGlossPower;
            public float nipGlossPower;
            public float skinGlossPower;

            public float cheekGlossPower;
            public float faceDetailPower;
            public float eyelineUpWeight;
            public float hlDownY;
            public float hlUpY;
            public float lipGlossPower;
            public float pupilHeight;
            public float pupilWidth;
        }
    }
}