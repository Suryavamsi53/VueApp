using System;

namespace WcfService.Models
{
    public class Lookup
    {
        public int Lookup_Id { get; set; }
        public string Lookup_Type { get; set; }
        public string Lookup_Desc { get; set; }
        public string Is_Active { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
    }
}