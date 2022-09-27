namespace Activity2API.Models.DTO
{
    public class QuestionDTO
    {
        public string Text { get; set; }
        public List<AnswerDTO> Answers { get; set; }
    }
}
