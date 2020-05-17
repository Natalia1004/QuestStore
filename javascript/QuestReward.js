var landingContent = document.getElementById("landing__content");

class QuestReward{
    constructor(title, cost, description){
        this.title = title;
        this.cost = cost;
        this.description = description;
    }
    printQuest(){
        let QuestReward = document.createElement('div');
        let Title = document.createElement('h3');
        let Cost = document.createElement('h3');
        let Description = document.createElement('p');
        //css
        QuestReward.className = "questreward";
        Title.className = "questreward__title";
        Cost.className = "questreward__cost";
        Description.className = "questreward__description";
        //values
        Title.innerText = this.title;
        Cost.innerText = this.cost;
        Description.innerText = this.description;
        //creating proper div
        QuestReward.appendChild(Title);
        QuestReward.appendChild(Cost);
        QuestReward.appendChild(Description);
        //adding quest to landing
        landingContent.appendChild(QuestReward);
    }
}

function printAllQuests(){
    let Quests = [
    new QuestReward("Spotter", "50cc", "Spot a major mistake in the assignment."),
    new QuestReward("Demo Master", "100cc", "Doing a demo for the class (side project, new technology, ...)."),
    new QuestReward("Screening", "100cc", "Taking part in the student screening process."),
    new QuestReward("Teacher", "400cc", "Organizing a workshop for other students."),
    new QuestReward("Wake up call", "300cc", "Attend 1 months without being late."),
    new QuestReward("New Me", "500-1000cc", "Set up a SMART goal accepted by a mentor, then achieve it."),
    new QuestReward("Best in field", "500cc", "Students choose the best project of the week. Selected team scores."),
    new QuestReward("Presenting FTW", "500cc", "Do a presentation on a meet-up."),
    ]

    for (i = 0; i < Quests.length; i++) {
        Quests[i].printQuest();
    }
}

printAllQuests();
