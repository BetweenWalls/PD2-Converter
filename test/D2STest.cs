using D2SLib;
using D2SLib.Model.Save;
using D2SLib.Model.TXT;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text.Json;

namespace D2SLibTests
{
    [TestClass]
    public class D2STest
    {
        [TestMethod]
        public void VerifyCanReadComplex115Save()
        {
            D2S character = Core.ReadD2S(File.ReadAllBytes(@"Resources\D2S\1.15\DannyIsGreat.d2s"));
            Assert.IsTrue(character.Name == "DannyIsGreat");
            Assert.IsTrue(character.ClassId == 0x1);
            /*
            File.WriteAllText(@"D:\DannyIsGreat.json", JsonSerializer.Serialize(character,
            new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreNullValues = true
            }));
            */
        }

        [TestMethod]
        public void VerifyCanWriteComplex115Save()
        {
            byte[] input = File.ReadAllBytes(@"Resources\D2S\1.15\DannyIsGreat.d2s");
            D2S character = Core.ReadD2S(input);
            byte[] ret = Core.WriteD2S(character);
            //File.WriteAllBytes(Environment.ExpandEnvironmentVariables($"%userprofile%/Saved Games/Diablo II Resurrected Tech Alpha/{character.Name}.d2s"), ret);
            Assert.IsTrue(input.Length == ret.Length);
        }

    }
}
