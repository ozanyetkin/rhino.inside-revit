using Autodesk.Revit.DB;

namespace RhinoInside.Revit.External.DB
{
  //  View Descipline parameters can have Null value
  // this enum wraps the DB.ViewDiscipline and adds a NotSet option
  public enum ViewDiscipline
  {
    NotSet = -1,
    Architectural = Autodesk.Revit.DB.ViewDiscipline.Architectural,
    Structural = Autodesk.Revit.DB.ViewDiscipline.Structural,
    Mechanical = Autodesk.Revit.DB.ViewDiscipline.Mechanical,
    Electrical = Autodesk.Revit.DB.ViewDiscipline.Electrical,
    Plumbing = Autodesk.Revit.DB.ViewDiscipline.Plumbing,
    Coordination  = Autodesk.Revit.DB.ViewDiscipline.Coordination
  }
}
