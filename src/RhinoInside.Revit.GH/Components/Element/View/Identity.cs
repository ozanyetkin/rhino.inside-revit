using System;
using Grasshopper.Kernel;
using DB = Autodesk.Revit.DB;

namespace RhinoInside.Revit.GH.Components
{
  public class ViewIdentity : AnalysisComponent
  {
    public override Guid ComponentGuid => new Guid("B0440885-4AF3-4890-8E84-3BC2A8342B9F");
    public override GH_Exposure Exposure => GH_Exposure.primary;
    protected override string IconTag => "ID";

    public ViewIdentity() : base
    (
      "View Identity", "Identity",
      "Query view identity information",
      "Revit", "View"
    )
    { }

    protected override void RegisterInputParams(GH_InputParamManager manager)
    {
      manager.AddParameter(new Parameters.View(), "View", "View", string.Empty, GH_ParamAccess.item);
    }

    protected override void RegisterOutputParams(GH_OutputParamManager manager)
    {
      manager.AddParameter(new Parameters.Param_Enum<Types.ViewDiscipline>(), "Discipline", "Discipline", "View discipline", GH_ParamAccess.item);
      // DB.ViewFamily is safer than DB.ViewType
      // see notes on RhinoInside.Revit.GH.Components.DocumentViews
      manager.AddParameter(new Parameters.Param_Enum<Types.ViewSystemFamily>(), "View System Family", "VSF", "View System Family", GH_ParamAccess.item);

      manager.AddTextParameter("Name", "N", "View name", GH_ParamAccess.item);
      manager.AddTextParameter("Title On Sheet", "TOS", "View title on sheet", GH_ParamAccess.item);
      manager.AddParameter(new Parameters.View(), "Template", "T", "View template", GH_ParamAccess.list);
      manager.AddBooleanParameter("IsTemplate", "T?", "View is template", GH_ParamAccess.item);
      manager.AddBooleanParameter("IsAssembly", "A?", "View is assembly", GH_ParamAccess.item);
      manager.AddBooleanParameter("IsPrintable", "P?", "View is printable", GH_ParamAccess.item);
      manager.AddParameter(new Parameters.View(), "Parent View", "PV", "Parent view if given view is dependent", GH_ParamAccess.item);
    }

    protected override void TrySolveInstance(IGH_DataAccess DA)
    {
      var view = default(DB.View);
      if (!DA.GetData("View", ref view))
        return;

      if (view.HasViewDiscipline())
        DA.SetData("Discipline", view.Discipline);
      else
        DA.SetData("Discipline", null);

      DB.ViewFamilyType viewType = view.Document.GetElement(view.GetTypeId()) as DB.ViewFamilyType;
      DA.SetData("View System Family", new Types.ViewSystemFamily(viewType.ViewFamily));
      PipeHostParameter(DA, view, DB.BuiltInParameter.VIEW_NAME, "Name");
      PipeHostParameter(DA, view, DB.BuiltInParameter.VIEW_DESCRIPTION, "Title On Sheet");
      DA.SetData("Template", view.Document.GetElement(view.ViewTemplateId) as DB.View);
      DA.SetData("IsTemplate", view.IsTemplate);
      DA.SetData("IsAssembly", view.IsAssemblyView);
      DA.SetData("IsPrintable", view.CanBePrinted);
      DA.SetData("Parent View", view.Document.GetElement(view.GetPrimaryViewId()) as DB.View);
    }
  }
}
