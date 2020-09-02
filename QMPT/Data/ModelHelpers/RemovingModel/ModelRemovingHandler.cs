using System;

namespace QMPT.Data.ModelHelpers.RemovingModel
{
    public static class ModelRemovingHandler
    {
        public static void OnRemove(IRemovable removableModel, int removerId)
        {
            removableModel.RemoverId = removerId;
            removableModel.RemovalTime = DateTime.UtcNow;
            removableModel.IsRemoved = true;
        }

        public static void Copy(IRemovable newRemovableModel, IRemovable oldRemovableModel)
        {
            newRemovableModel.RemovalTime = oldRemovableModel.RemovalTime;
            newRemovableModel.RemoverId = oldRemovableModel.RemoverId;
            newRemovableModel.IsRemoved = oldRemovableModel.IsRemoved;
        }
    }
}
