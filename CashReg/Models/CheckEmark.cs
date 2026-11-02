namespace CashReg.Models
{
    public class RequestCheckEmark
    {
        /// <summary>
        /// Հարցման հերթական համարը
        /// </summary>
        public int seq { get; private set; }

        /// <summary>
        /// emark կոդ 
        /// • ՀԴՄ–ին փոխանցվող emark կոդերի նիշերի քանակը պետք է լինի
        /// նվազագույն 29 (ներառյալ) և առավելագույն 110 (ներառյալ)
        /// • emark կոդերը պետք է պարունակեն միայն ASCII աղյուսակի սիմվոլներ,
        /// ASCII կոդ 33-ից(ներառյալ) մինչև ASCII կոդ 126 (ներառյալ) և ASCII կոդ 29։
        /// Կոդի մեջ առկա " սիմվոլը պետք է փոխարինել \"-ով, \ սիմվոլը պետք է
        /// փոխարինել \\ -ով, իսկ ASCII կոդ 29-ը պետք է փոխարինել \u001d –ով
        /// </summary>
        public string eMark { get; private set; }

        public RequestCheckEmark(int seqValue, string emark)
        {
            seq = seqValue;
            eMark = emark;
        }
    }
}
