namespace TransactionsApp.Application.Models.Dto
{
    /// <summary>
    /// Data transfer object for adding a new user.
    /// </summary>
    public class AddUserDto
    {
        /// <summary>
        /// Full name of the user in Hebrew.
        /// </summary>
        public string FullNameHebrew { get; set; }

        /// <summary>
        /// Full name of the user in English.
        /// </summary>
        public string FullNameEnglish { get; set; }

        /// <summary>
        /// User's date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// User's unique identity (e.g., Teudat Zehut, National ID).
        /// </summary>
        public string UserIdentity { get; set; }
    }
}
