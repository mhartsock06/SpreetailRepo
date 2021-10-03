using Spreetail;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MVDUnitTests
{
    public class DataBookTests
    {
        [Fact]
        public void KeyExistsReturnsTrue()
        {
            DataBook book = new DataBook();
            SetupMVD(book);
            Assert.True(book.KeyExists("fruit"));
        }

        [Fact]
        public void KeyExistsReturnsFalse()
        {
            DataBook book = new DataBook();
            SetupMVD(book);
            Assert.False(book.KeyExists("notafruit"));
        }

        [Fact]
        public void AddMemberSuccess()
        {
            DataBook book = new DataBook();
            SetupMVD(book);

            book.AddMember("fruit", "pineapple");
            book.DataMemberList.TryGetValue("fruit", out HashSet<string> fruitValues);

            Assert.Contains("pineapple", fruitValues);
        }

        [Fact]
        public void RemoveMemberSuccess()
        {
            DataBook book = new DataBook();
            SetupMVD(book);

            book.RemoveMember("fruit", "apple");
            book.DataMemberList.TryGetValue("fruit", out HashSet<string> fruitValues);

            Assert.DoesNotContain("apple", fruitValues);
        }

        [Fact]
        public void RemoveMemberKeyDoesNotExist()
        {
            DataBook book = new DataBook();
            SetupMVD(book);

            (bool keyExists, _, _, _) = book.RemoveMember("doesnotexist", "apple");

            Assert.False(keyExists);
        }

        [Fact]
        public void RemoveMemberMemberDoesNotExist()
        {
            DataBook book = new DataBook();
            SetupMVD(book);

            (_, bool? memberExists, _, _) = book.RemoveMember("fruit", "jellybeans");

            Assert.False(memberExists);
        }

        [Fact]
        public void RemoveAllMembersSuccess()
        {
            DataBook book = new DataBook();
            SetupMVD(book);

            (bool keyExists, _, bool success, _) = book.RemoveAllMembers("fruit");

            Assert.True(keyExists);
            Assert.True(success);
            Assert.True(book.DataMemberList.Keys.Count == 1);
            Assert.False(book.DataMemberList.ContainsKey("fruit"));
        }

        [Fact]
        public void RemoveAllMembersError()
        {
            DataBook book = new DataBook();
            SetupMVD(book);

            (bool keyExists, _, bool success, _) = book.RemoveAllMembers("Notarealkey");

            Assert.False(keyExists);
            Assert.False(success);
            Assert.True(book.DataMemberList.Keys.Count == 2);

        }

        [Fact]
        public void ClearSuccess()
        {
            DataBook book = new DataBook();
            SetupMVD(book);

            bool success = book.ClearEntries();

            Assert.True(book.DataMemberList.Keys.Count == 0);
            Assert.True(success);
        }

        [Fact]
        public void ListKeysSuccess()
        {
            DataBook book = new DataBook();
            SetupMVD(book);

            List<string> results = book.ListKeys().ToList();

            Assert.True(results.Count == 2);
            Assert.Contains("fruit", results);
            Assert.Contains("vegetable", results);
        }

        [Fact]
        public void ListMembersSuccess()
        {
            DataBook book = new DataBook();
            SetupMVD(book);

            (bool keyExists, _, bool success, IEnumerable<string> resultSet) = book.ListMembers("fruit");

            Assert.True(keyExists);
            Assert.True(success);
            Assert.True(resultSet.ToList().Count == 4);
        }

        [Fact]
        public void ListMembersKeyNotExists()
        {
            DataBook book = new DataBook();
            SetupMVD(book);

            (bool keyExists, _, _, IEnumerable<string> resultSet) = book.ListMembers("doesnotexist");

            Assert.False(keyExists);
            Assert.Null(resultSet);
        }


        private void SetupMVD(DataBook book)
        {
            book.DataMemberList = new Dictionary<string, HashSet<string>>
            {
                {
                    "fruit",
                    new HashSet<string>
                    {
                        {"apple"},
                        {"orange" },
                        {"grape" },
                        {"kiwi" }
                    }
                },
                {
                    "vegetable",
                    new HashSet<string>
                    {
                        {"carrot"},
                        {"cucumber" },
                        {"pepper" },
                        {"celery" }
                    }
                }
            };
        }
    }
}
