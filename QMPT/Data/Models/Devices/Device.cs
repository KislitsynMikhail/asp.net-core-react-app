using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QMPT.Data.ModelHelpers.EditingModel;
using QMPT.Data.ModelHelpers.RemovingModel;
using QMPT.Data.ModelRestrictions.Devices;
using QMPT.Models.Requests.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace QMPT.Data.Models.Devices
{
    public class Device : BaseModel, IEditable<Device>, IRemovable, ICloneable
    {
        #region Columns
        [MaxLength(DeviceRestrictions.numberMaxLength)]
        public string Number { get; set; }
        [MaxLength(DeviceRestrictions.measurementRangeMaxLength)]
        public string MeasurementRange { get; set; }
        [MaxLength(DeviceRestrictions.permissibleSystematicErrorMaxMaxLength)]
        public string PermissibleSystematicErrorMax { get; set; }
        [Column(TypeName = "jsonb")]
        public List<AdmissibleRandomErrorMax> AdmissibleRandomErrorsMax { get; set; }
       
        [MaxLength(DeviceRestrictions.magnetometerReadingsVariationMaxLength)]
        public string MagnetometerReadingsVariation { get; set; }
        [MaxLength(DeviceRestrictions.gradientResistenceMaxLength)]
        public string GradientResistance { get; set; }
        [MaxLength(DeviceRestrictions.signalAmplitudeMaxLength)]
        public string SignalAmplitude { get; set; }
        [MaxLength(DeviceRestrictions.relaxtionTimeMaxLength)]
        public string RelaxationTime { get; set; }
        [MaxLength(DeviceRestrictions.optimalCycleMaxLength)]
        public string OptimalCycle { get; set; }

        #region Editable
        [ForeignKey(nameof(Original))]
        public int? OriginalId { get; set; }
        public Device Original { get; set; }
        [ForeignKey(nameof(Editor))]
        public int? EditorId { get; set; }
        public DateTime? EditingTime { get; set; }
        public bool IsEdited { get; set; }
        public int Version { get; set; }
        public bool IsRelevant { get; set; }
        public User Editor { get; set; }
        #endregion Editable
        #region Removable
        [ForeignKey(nameof(Remover))]
        public int? RemoverId { get; set; }
        public DateTime? RemovalTime { get; set; }
        public bool IsRemoved { get; set; }
        public User Remover { get; set; }
        #endregion Removable
        #endregion Columns

        public string CurrentVersionKey => $"{nameof(Device)}_{nameof(CurrentVersionKey)}_{OriginalId}";

        public void CreateNewVersionValue()
        {
            KeyValuePair.CreateNewKeyValuePair(CurrentVersionKey, "1");
        }

        public static void OnModelCreating(EntityTypeBuilder<Device> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(e => e.Creator)
                .WithMany(u => u.CreatedDevices)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(e => e.Editor)
                .WithMany(u => u.EditedDevices)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(e => e.Remover)
                .WithMany(u => u.RemovedDevices)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public Device() { }

        public Device(Device device) : base(device.CreatorId)
        {
            Number = device.Number;
            MeasurementRange = device.Number;
            PermissibleSystematicErrorMax = device.PermissibleSystematicErrorMax;
            AdmissibleRandomErrorsMax = device.AdmissibleRandomErrorsMax;
            MagnetometerReadingsVariation = device.MagnetometerReadingsVariation;
            GradientResistance = device.GradientResistance;
            SignalAmplitude = device.SignalAmplitude;
            RelaxationTime = device.RelaxationTime;
            OptimalCycle = device.OptimalCycle;

            ModelEditingHandler.OnCopy(this, device);
            ModelRemovingHandler.Copy(this, device);
        }

        public Device(DeviceRequest deviceRequest, int creatorId) : base(creatorId)
        {
            ModelEditingHandler.OnCreate(this);
            CopyData(deviceRequest);
        }

        public void ChangeData(object newData, int editorId, int newVersion)
        {
            CopyData(newData as DeviceRequest);

            ModelEditingHandler.OnEdit(this, editorId, newVersion);
        }

        public object Clone()
        {
            return new Device(this);
        }

        private void CopyData(DeviceRequest deviceRequest)
        {
            Number = deviceRequest.Number;
            MeasurementRange = deviceRequest.MeasurementRange;
            PermissibleSystematicErrorMax = deviceRequest.PermissibleSystematicErrorMax;
            AdmissibleRandomErrorsMax = deviceRequest.AdmissibleRandomErrorsMax
                .Select(val => new AdmissibleRandomErrorMax(val))
                .ToList();
            MagnetometerReadingsVariation = deviceRequest.MagnetometerReadingsVariation;
            GradientResistance = deviceRequest.GradientResistance;
            SignalAmplitude = deviceRequest.SignalAmplitude;
            RelaxationTime = deviceRequest.RelaxationTime;
            OptimalCycle = deviceRequest.OptimalCycle;
        }
    }
}
