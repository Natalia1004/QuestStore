// JavaScript source code



function person(fName, sName, userFunction, description) {
    this.fName = fName;
    this.sName = sName;
    this.userFunction = userFunction;
    this.description = description;
}

let positions = ["Head", "Senior Mentor", "Mentor", "Junior Mentor"];
let descriptions = [
    "Saturday's sunshine and warm spring weather was exactly what local golfers wanted as area courses opened for the 2020 season",
    "Texans might consider the possibilities of moderating their recent habits by going out for dinner",
    "Princess Anne has become the subject of speculation after a video showed the Queen seemingly gesturing to her daughter",
    "New algorithm predicts optimal materials with required properties among all possible compounds"
];

let person1 = new person("Sam",
    "Serious",
    positions[0],
    descriptions[0]);

let person2 = new person("Jerry",
    "Pythonist",
    positions[1],
    descriptions[1]);

let person3 = new person("Anne",
    "Javascript",
    positions[2],
    descriptions[2]);

let person4 = new person("Tim",
    "Algor",
    positions[3],
    descriptions[3]);


var persons = [person1, person2, person3, person4];

function LoadPersonDescription(index, person) {
    let injectionPos = document.getElementsByClassName("show-all_box")[index];
    let personDescription = document.createElement("p");
    personDescription.className = "show-all_text";
    personDescription.innerHTML = `${person.description}`;
    injectionPos.append(personDescription);
}

function LoadPosition(index, person) {
    let injectionPos = document.getElementsByClassName("show-all_user")[index];
    let injectionFunction = document.createElement("h4");
    injectionFunction.className = "user_position";
    injectionFunction.innerHTML = `${person.userFunction}`;
    injectionPos.append(injectionFunction);
}


function LoadName(index, person) {
    let injectionPos = document.getElementsByClassName("show-all_user")[index];
    let userName = document.createElement("h3");
    userName.className = "user_name";
    userName.innerHTML = `${person.fName} <br> ${person.sName}`;
    injectionPos.append(userName);
}


function LoadDivs(index) {
    let injectionPos = document.getElementsByClassName("show-all_box")[index];
    let showAllUser = document.createElement("div");
    showAllUser.className = "show-all_user";
    injectionPos.appendChild(showAllUser);
}


function LoadIntoLanding() {
    let injectionPos = document.querySelector(".inside_landing");

    for (let index = 0; index < persons.length; index++) {
        let showAllBox = document.createElement("div");
        showAllBox.className = "show-all_box";
        injectionPos.appendChild(showAllBox);
        element = persons[index];
        LoadDivs(index);
        LoadName(index, element);
        LoadPosition(index, element);
        LoadPersonDescription(index, element);
    }

}