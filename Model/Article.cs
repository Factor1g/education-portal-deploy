﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Article : Material
    {
        public DateTime PublicationDate { get; set; }
        public string Resource {  get; set; }       
    }
}
