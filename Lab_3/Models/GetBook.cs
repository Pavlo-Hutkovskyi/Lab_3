using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Models
{
    public class GetBook : BookId
    {
        public Book booking { get; set; }
    }
}
