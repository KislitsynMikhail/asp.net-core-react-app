using QMPT.Data.ModelHelpers.RemovingModel;

namespace QMPT.Models.Responses.Helpers.RemovingModel
{
    public static class RemovableModelResponseFiller
    {
        public static void Fill(IRemovableResponse removableResponse, IRemovable removableModel)
        {
            removableResponse.IsRemoved = removableModel.IsRemoved;
            removableResponse.RemovalTime = removableModel.RemovalTime;
            removableResponse.RemoverId = removableModel.RemoverId;
        }
    }
}
