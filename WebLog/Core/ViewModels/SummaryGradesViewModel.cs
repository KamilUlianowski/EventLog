using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLog.Core.ViewModels
{
    public class SummaryGradesViewModel
    {
      public  Dictionary<int, int> grades = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 } };
       public Dictionary<string, Dictionary<string, List<int>>> dict =
            new Dictionary<string, Dictionary<string, List<int>>>();
    }
}