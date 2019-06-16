﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Models
{
    public class House : Item
    {
        public string region { get; set; }
        public string coatOfArms { get; set; }
        public string words { get; set; }
        public string[] titles { get; set; }
        public string[] seats { get; set; }
        public string currentLord { get; set; }
        public string heir { get; set; }
        public string overlord { get; set; }
        public string founded { get; set; }
        public string founder { get; set; }
        public string diedOut { get; set; }
        public string[] ancestralWeapons { get; set; }
        public string[] cadetBranches { get; set; }
        public string[] swornMembers { get; set; }
    }
}
