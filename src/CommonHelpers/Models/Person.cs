using System;
using CommonHelpers.Common;

namespace CommonHelpers.Models
{
    public class Person : BindableBase
    {
        private string name;
        private int age;
        private GenderType gender;
        private DateTime dateOfBirth;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public int Age
        {
            get => age;
            set => SetProperty(ref age, value);
        }

        public GenderType Gender
        {
            get => gender;
            set => SetProperty(ref gender, value);
        }

        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set => SetProperty(ref dateOfBirth, value);
        }
    }
}