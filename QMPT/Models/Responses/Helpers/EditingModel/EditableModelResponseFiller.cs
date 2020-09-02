using QMPT.Data.ModelHelpers.EditingModel;

namespace QMPT.Models.Responses.Helpers.EditingModel
{
    public static class EditableModelResponseFiller
    {
        public static void Fill(IEditableResponse editableResponse, IEditable editableModel)
        {
            editableResponse.EditorId = editableModel.EditorId;
            editableResponse.EditingTime = editableModel.EditingTime;
            editableResponse.IsEdited = editableModel.IsEdited;
        }
    }
}
