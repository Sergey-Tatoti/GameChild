using System.Collections.Generic;

namespace SaveData
{
    [System.Serializable]
    public class GameData
    {
        public string Name = "Name1234";
        public int Experience;
        public int Level = 1;
        public List<int> IdOpenItems = new List<int> { 1, 2, 3, 4, 5, 6};
        public List<int> IdSelectedItems = new List<int> { 1, 2, 3, 4, 5, 6 };
        public List<int> IdShowedItems = new List<int> { 1, 2, 3, 4, 5, 6 };
    }
}