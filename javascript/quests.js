var landingContent = document.getElementById("landing__content");

var quest = document.createElement('div');
var questTitle = document.createElement('h3');
var questCost = document.createElement('h3');
var questDescription = document.createElement('p');

quest.className = "quest";
questTitle.className = "quest__title";
questCost.className = "quest__cost";
questDescription.className = "quest__description";

questTitle.innerText = "Test Title"
questCost.innerText = "2000cc";
questDescription.innerText = "Short description of current quest";

quest.appendChild(questTitle);
quest.appendChild(questCost);
quest.appendChild(questDescription);

landingContent.appendChild(quest);
