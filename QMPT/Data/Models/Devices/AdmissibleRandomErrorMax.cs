using QMPT.Models.Requests.Devices;

namespace QMPT.Data.Models.Devices
{
    public class AdmissibleRandomErrorMax
    {
        public string Seconds { get; set; }
        public string Value { get; set; }

        public AdmissibleRandomErrorMax() { }

        public AdmissibleRandomErrorMax(AdmissibleRandomErrorsRequest admissibleRandomErrorsRequest)
        {
            Seconds = admissibleRandomErrorsRequest.Seconds;
            Value = admissibleRandomErrorsRequest.Value;
        }
    }
}
