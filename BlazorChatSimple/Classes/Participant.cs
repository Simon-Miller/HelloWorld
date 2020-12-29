using System.ComponentModel.DataAnnotations;

namespace BlazorChatSimple.Classes
{
    public class Participant
    {
        [Required]
        [RegularExpression(@"[A-Za-z '-]{1,20}", ErrorMessage = "No funny characters!  a-z, ' - and that's all.")]
        public string DisplayName { get; set; }

        public int NumberOfPosts = 0;
    }
}
