using Activity2API.Models;

namespace Activity2API
{
    public static class State
    {
        public static List<User> Users { get; set; } = new List<User>();
        public static List<Question> Questions { get; set; } = new List<Question>();
    }
}
