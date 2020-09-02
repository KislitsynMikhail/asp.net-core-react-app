using QMPT.Data.Models;
using QMPT.Data.Services;
using QMPT.Helpers;
using System;

namespace QMPT.Data.ModelHelpers.EditingModel
{
    public static class ModelEditingHandler
    {
        public static void OnEdit(IEditable editableModel, int editorId, int version)
        {
            editableModel.EditorId = editorId;
            editableModel.Version = version;
            editableModel.EditingTime = DateTime.UtcNow;
            editableModel.IsEdited = true;
        }

        public static void OnCopy(IEditable newEditableModel, IEditable oldEditableModel)
        {
            newEditableModel.EditingTime = oldEditableModel.EditingTime;
            newEditableModel.EditorId = oldEditableModel.EditorId;
            newEditableModel.Version = oldEditableModel.Version;
            newEditableModel.IsEdited = oldEditableModel.IsEdited;
            newEditableModel.IsRelevant = false;
            newEditableModel.OriginalId = oldEditableModel.OriginalId;
        }

        public static void OnCreate(IEditable model)
        {
            model.IsRelevant = true;
            model.Version = 1;
        }

        public static void Insert<T>(IBaseOperation<T> dataService, T model) where T : IEditable, ICloneable
        {
            dataService.Insert(model);
            model.OriginalId = (model as BaseModel).Id;
            dataService.Update(model);
            model.CreateNewVersionValue();
        }

        public static void Update<T>(IBaseOperation<T> dataService, object requestData, T model, int editorId) where T : IEditable, ICloneable
        {
            var oldModel = model.Clone();
            dataService.Insert(oldModel);

            var keyValuePairsService = new KeyValuePairsService();
            var keyValuePair = keyValuePairsService
                .Get(model.CurrentVersionKey);

            var newVersion = keyValuePair.Value.ToInt() + 1;
            model.ChangeData(requestData, editorId, newVersion);
            dataService.Update(model);

            keyValuePair.Value = newVersion.ToString();
            keyValuePairsService.Update(keyValuePair);
        }
    }
}
