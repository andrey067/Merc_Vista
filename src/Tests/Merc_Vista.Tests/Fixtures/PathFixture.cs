using Domain.Dtos;

namespace Merc_Vista.Tests.Fixtures
{
    public class PathFixture: IDisposable
    {
        private string DirectoryPath { get; set; }

        public string PathEmpity()
        {
            DirectoryPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(DirectoryPath);
            return DirectoryPath;
        }

        public (string, string[]) PathWithFile()
        {
            DirectoryPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(DirectoryPath);

            File.WriteAllText(Path.Combine(DirectoryPath, "file1.csv"), "coluna1,coluna2,coluna3\n1,2,3\n4,5,6");
            File.WriteAllText(Path.Combine(DirectoryPath, "file2.csv"), "coluna1,coluna2,coluna3\n7,8,9\n10,11,12");

            return (DirectoryPath, Directory.GetFiles(DirectoryPath, "*.csv"));
        }

        public void Dispose() => Directory.Delete(DirectoryPath, true);
    }
}
