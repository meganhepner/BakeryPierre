using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakeryPierre.ViewModels
{
  public class EditRoleViewModel
  {
    public int Id { get; set;}
    public string RoleName { get; set; }
    public List<string> Users { get; set; }

  }
}