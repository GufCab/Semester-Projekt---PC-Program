﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaReader.FileIndexer;
using MetaReader.MetadataReader;

namespace LocalDbTester
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var dbh = new dbclases.LocalDbhandel();

            dbh.FillIP("192.168.1.11");

            var ha = new List<string>();
            ha.Add(@"C:\Users\Malmos\Documents\GitHub\Semester-Projekt---PC-Program\musikmappe");

            dbh.Fillrest(ha);

            
            
        }
    }
}
