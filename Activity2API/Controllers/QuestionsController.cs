using Microsoft.AspNetCore.Mvc;
using Activity2API.Models;
using Activity2API.Models.DTO;

namespace Activity2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Question>> FindAll()
        {
            return Ok(State.Questions);
        }

        [HttpGet("{id}")]
        public ActionResult<Question> FindById([FromRoute(Name = "id")] int id)
        {
            var question = State.Questions.Find(question => question.Id == id);
            if (question == null) return NotFound("Question not found");
            return Ok(question);
        }

        [HttpPost]
        public ActionResult<Question> Create([FromBody] QuestionDTO questionDTO)
        {
            // Setup question's answers
            var answers = new List<Answer>();
            questionDTO.Answers.ForEach(answerDTO =>
            {
                Answer answer = new Answer()
                {
                    Id = answers.Count,
                    Text = answerDTO.Text
                };
                answers.Add(answer);
            });

            // Setup question
            var question = new Question()
            {
                Id = State.Questions.Count,
                Text = questionDTO.Text,
                Answers = answers
            };

            State.Questions.Add(question);
            return Ok(question);
        }
    }
}
