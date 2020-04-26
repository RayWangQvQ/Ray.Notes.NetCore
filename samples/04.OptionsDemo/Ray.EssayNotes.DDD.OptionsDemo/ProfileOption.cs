using System;

namespace Ray.EssayNotes.DDD.OptionsDemo
{
    public class ProfileOption
    {
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public ContactInfo ContactInfo { get; set; }

        public ProfileOption()
        {
            Console.WriteLine($"【Create Instance】{this.GetType().Name}:{this.GetHashCode()}");
        }
    }

    public class ContactInfo
    {
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
