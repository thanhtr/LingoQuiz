using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;

public class XMLManager
{
    public List<Quiz> LoadXml(string xml)
    {
		Debug.Log("LoadXml() xml: "+xml);
		List<Quiz> quizesList = new List<Quiz>();

        List<XmlNodeList> quizNodeListCollection = new List<XmlNodeList>();
        List<XmlNodeList> questionNodeListCollection = new List<XmlNodeList>();
        List<XmlNodeList> answerNodeListCollection = new List<XmlNodeList>();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml);
        XmlNodeList quizNodes = xmlDoc.GetElementsByTagName("quiz");
        XmlNodeList questionNodes = null;
        XmlNodeList answerNodes = null;

        for (int i = 0; i < quizNodes.Count; i++)
        {
            Quiz quiz = new Quiz();
			quiz.id = GetNodeValue(quizNodes[i], "id");
			quiz.intro = GetNodeValue(quizNodes[i], "intro");
			quiz.name = GetNodeValue(quizNodes[i], "name");
			quiz.level = (QuizDifficultyLevelEnum)(int.Parse(GetNodeValue(quizNodes[i], "level")));
			quiz.version = GetNodeValue(quizNodes[i], "version");
			quiz.category = GetNodeValue(quizNodes[i], "category");

			quiz.level = QuizDifficultyLevelEnum.Easy; //(1; //(QuizDifficultyLevelEnum) (int.Parse(quizNodes[i].Attributes["level"].Value));

            questionNodes = quizNodes[i].ChildNodes;
            for (int j = 0; j < questionNodes.Count; j++)
            {
                Question question = new Question();
				question.queId =  GetNodeValue(questionNodes[j], "que_id");
				question.text = GetNodeValue(questionNodes[j], "text");
				question.correctAnswerId = GetNodeValue(questionNodes[j], "correct_answer_id");
				question.explanation = GetNodeValue(questionNodes[j], "explanation");
				question.isInput = GetNodeValue(questionNodes[j], "isinput") == "True";
				
				answerNodes = questionNodes[j].ChildNodes;
                for (int k = 0; k < answerNodes.Count; k++)
                {
                    Answer answer = new Answer();
					answer.answerId = GetNodeValue(answerNodes[k], "answer_id");
					answer.text = GetNodeValue(answerNodes[k], "text");
					answer.quizId = GetNodeValue(answerNodes[k], "quiz_id");
					answer.queId = GetNodeValue(answerNodes[k], "que_id");
                    question.answers.Add(answer);
                }
                quiz.questions.Add(question);
            }
            quizesList.Add(quiz);
        }
	
        return quizesList;
    }

	public string WriteToString(Quiz quiz)
	{
		StringBuilder quizStr = new StringBuilder();
		using (XmlWriter writer = XmlWriter.Create(quizStr))
		{
			writer.WriteStartDocument();
			writer.WriteStartElement("quizzes");

			writer.WriteStartElement("quiz");
			writer.WriteAttributeString("id", quiz.id);
			writer.WriteAttributeString("intro", quiz.intro);
			writer.WriteAttributeString("name", quiz.name);
			writer.WriteAttributeString("version", quiz.version);
			writer.WriteAttributeString("category", quiz.category);

			int levelInt = (int)quiz.level;
			writer.WriteAttributeString("level", levelInt.ToString());

			foreach (Question question in quiz.questions)
			{
				writer.WriteStartElement("question");
				writer.WriteAttributeString("que_id", question.queId);
				writer.WriteAttributeString("text", question.text);
				writer.WriteAttributeString("correct_answer_id", question.correctAnswerId);
				writer.WriteAttributeString("explanation", question.explanation);
				writer.WriteAttributeString("isinput", question.isInput.ToString());

				foreach (Answer answer in question.answers)
				{
					writer.WriteStartElement("answer");
					writer.WriteAttributeString("answer_id", answer.answerId);
					writer.WriteAttributeString("text", answer.text);
					writer.WriteAttributeString("que_id", answer.queId);
					writer.WriteAttributeString("quiz_id", answer.quizId);
					writer.WriteEndElement();
				}

				writer.WriteEndElement();	// </question>
			}

			writer.WriteEndElement();		// </quiz>
			writer.WriteEndDocument();
		}

		return quizStr.ToString();
	}

	public string GetNodeValue(XmlNode node, string attributeName)
	{
		string value = "";
		if (node.Attributes != null)
		{
			XmlAttribute attribute = node.Attributes[attributeName];
			if (attribute != null) {
				value = attribute.Value;
			}
			else {
				Debug.LogWarning("ERROR in xml: Node '" + attributeName + "' not found.");
			}
		}
		return value;
	}
}
