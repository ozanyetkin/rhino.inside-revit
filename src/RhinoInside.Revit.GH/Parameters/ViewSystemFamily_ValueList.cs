using System;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;
using DB = Autodesk.Revit.DB;

namespace RhinoInside.Revit.GH.Parameters
{
  public class ViewSystemFamily_ValueList : GH_ValueList
  {
    public override Guid ComponentGuid => new Guid("E4256D4B-F3F1-4229-940D-68DA13569103");
    public override GH_Exposure Exposure => GH_Exposure.quarternary;

    public ViewSystemFamily_ValueList()
    {
      Category = "Revit";
      SubCategory = "Input";
      Name = "View System Family";
      NickName = "VSF";
      Description = "Picker for builtin view system families";

      ListItems.Clear();

      foreach(var value in Enum.GetValues(typeof(DB.ViewFamily)))
        ListItems.Add(new GH_ValueListItem(value.ToString().Humanify(), ((int) value).ToString()));
    }
  }
}
