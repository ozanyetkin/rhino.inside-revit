using System;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;
using DBX = RhinoInside.Revit.External.DB;

namespace RhinoInside.Revit.GH.Parameters
{
  public class ViewDiscipline_ValueList : GH_ValueList
  {
    public override Guid ComponentGuid => new Guid("B336A87E-AA97-417C-8AF1-8A7263B3D97A");
    public override GH_Exposure Exposure => GH_Exposure.quarternary;

    public ViewDiscipline_ValueList()
    {
      Category = "Revit";
      SubCategory = "Input";
      Name = "View Discipline";
      NickName = "VD";
      Description = "Picker for builtin view discipline";

      ListItems.Clear();

      foreach(var value in Enum.GetValues(typeof(DBX.ViewDiscipline)))
        ListItems.Add(new GH_ValueListItem(value.ToString().Humanify(), ((int) value).ToString()));
    }
  }
}
