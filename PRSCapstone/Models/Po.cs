namespace PRSCapstone.Models {
    public class Po {

        public Vendor Vendor { get; set; } = null!;
        public IEnumerable<PoLine> PoLines { get; set; } = null!;
        public decimal PoTotal { get; set; } = 0;
    }
}
