var landingContent = document.getElementById("landing__content");

class Quest {
    constructor(title, cost, description){
        this.title = title;
        this.cost = cost;
        this.description = description;
    }
    printQuest(){
        let quest = document.createElement('div');
        let questTitle = document.createElement('h3');
        let questCost = document.createElement('h3');
        let questDescription = document.createElement('p');
        //css
        quest.className = "quest";
        questTitle.className = "quest__title";
        questCost.className = "quest__cost";
        questDescription.className = "quest__description";
        //values
        questTitle.innerText = this.title;
        questCost.innerText = this.cost;
        questDescription.innerText = this.description;
        //creating proper div
        quest.appendChild(questTitle);
        quest.appendChild(questCost);
        quest.appendChild(questDescription);
        //adding quest to landing
        landingContent.appendChild(quest);
    }
}

let Quests = [
    new Quest("Spotter", "50cc", "Spot a major mistake in the assignment."),
    new Quest("Demo Master", "100cc", "Doing a demo for the class (side project, new technology, ...)."),
    new Quest("Screening", "100cc", "Taking part in the student screening process."),
    new Quest("Teacher", "400cc", "Organizing a workshop for other students."),
    new Quest("Wake up call", "300cc", "Attend 1 months without being late."),
    new Quest("New Me", "500-1000cc", "Set up a SMART goal accepted by a mentor, then achieve it."),
    new Quest("Best in field", "500cc", "Students choose the best project of the week. Selected team scores."),
    new Quest("Presenting FTW", "500cc", "Do a presentation on a meet-up."),
]

for (i = 0; i < Quests.length; i++) {
    Quests[i].printQuest();
}
