using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyGroup
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class CopyGroupPlagin : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Reference reference = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Выберите группу элементов");
            Element element =  doc.GetElement(reference);
            Group group = element as Group;
            XYZ point = uidoc.Selection.PickPoint("Выберите точку");
            Transaction transaction = new Transaction(doc);
            transaction.Start("Копирование групп");
            doc.Create.PlaceGroup(point, group.GroupType);
            transaction.Commit();
            return Result.Succeeded;
        }
    }
}
