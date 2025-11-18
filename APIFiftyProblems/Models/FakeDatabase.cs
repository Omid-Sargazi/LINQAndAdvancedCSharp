namespace APIFiftyProblems.Models
{
    public static class FakeDatabase
    {
           public static List<User> FakeUsers = new()
        {
            new User { Id = 1, UserName = "omid", PasswordHash = "1234", Role="admin", Region="north" },
            new User { Id = 2, UserName = "reza", PasswordHash = "pass", Role="editor", Region="south" }
        };
    }
}