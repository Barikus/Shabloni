using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Shabloni2.Models;

namespace Shabloni2
{
    internal static class DataTest
    {
        static List<ArchiveModel> testFiles = new List<ArchiveModel>
        {
            new ArchiveModel { Name = "Text", Extension = ".txt", Size = 822 }, // 0 неправильное расширение
            new ArchiveModel { Name = "UnpackMe", Extension = ".tar", Size = 109602 }, // 1
            new ArchiveModel { Name = "Hello", Extension = ".zip", Size = 900514 }, // 2
            new ArchiveModel { Name = "NotVirus", Extension = ".rar", Size = 314572800 }, // 3 вирус
            new ArchiveModel { Name = "JustPNG", Extension = ".zip", Size = 3221225472 }, // 4 большой вес
            new ArchiveModel { Name = "😦😦😦", Extension = ".rar", Size = 900 }, // 5 некорректное название
            new ArchiveModel { Name = "rfxr3ohtnoix34tyxoi54vu32yrnxito2u3rnyix2htmrioxugx2hoigoh2g", Extension = ".zip", Size = 54051 } // 6
        };

        public static List<ArchiveModel> GetData() { return testFiles; }
    }
}
