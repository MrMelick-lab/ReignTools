namespace ReignTools.Entities.Business
{
    public class Sets
    {
        public short Width { get; set; }
        public short Height { get; set; }

        public override string ToString()
        {
            if (Width == 0 || Height == 0)
            {
                return string.Empty;
            }

            return $"{Width}x{Height}";
        }
    }
}
