"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/pollster").build();

//HOOK UP SIGNALS FROM SERVER TO LOCAL METHODS:  ////////////////////////
//updates UI after votes from any client
connection.on("voteUpdated", updateResults);
//is retrieved when page is first opened, to get current poll results
connection.on("receivePollInfo", receivePollInfo);
//is retrieved when page is first opened, to get current poll results
connection.on("goToPoll", receiveGoToPoll);

////////////////////////////////////////////////////////////////////

//retrieve current poll status from server when connection is up
connection.start().then(function () {
    joinGroup(pollId);
    getUpdatedPollInfo();
}).catch(function (err) {
    return console.error(err.toString());
});

function joinGroup(pollId) {
    connection.invoke("JoinPoll", pollId).catch(function (err) {
        alert("Error joining polling group on startup");
    });
}

function sendToPoll(groupId, nextPollId) {
    connection.invoke("GoToPoll", groupId, nextPollId).catch(function (err) {
        alert("Error sending to next poll " + nextPollId);
    });
}

function getUpdatedPollInfo() {
    connection.invoke("GetPollInfo", pollId).catch(function (err) {
        alert("Error retrieving updated poll info");
    });
}

function updateResults(results) {
    for (let i = 0; i < results.length; i++) {
        let span = document.getElementById("choice" + i + "VoteCount");
        if (span.innerText != results[i]) {
            span.innerText = results[i];
            pulseElement(span);
        }
    }
}

function receiveGoToPoll(pollId) {
    window.location.href = "/poll/" + pollId;
}

function receivePollInfo(poll) {
    createPollButtons(poll);
    updateResults(poll.results);
}

function createPollButtons(poll) {
    let pollTitleDiv = document.querySelector("h1#title");
    pollTitleDiv.innerText = poll.title;
    let pollDescriptionDiv = document.querySelector("div#description");
    pollDescriptionDiv.innerText = poll.description;
    let choiceNumber = 0;
    let pollDiv = document.getElementById("results");
    let template = document.getElementsByTagName("template")[0];

    poll.choices.forEach(function (choice) {
    
        let divResult = template.content.querySelector(".result");
    let choiceText = divResult.querySelector(".choiceText");
        choiceText.innerText = choice ;
        let choiceVoteCount = divResult.querySelector(".choiceVoteCount");
        choiceVoteCount.id = "choice"+choiceNumber+"VoteCount";

    let updatedTemplate = document.importNode(divResult, true);
    pollDiv.appendChild(updatedTemplate);
        choiceNumber++;
    });

    //and hook up to the button click event
    document.querySelectorAll(".voteButton").forEach(
        (elem, index) => {
            elem.addEventListener("click", function () {
                connection.invoke("Vote", pollId, index).catch(function (err) {
                    return console.error(err.toString());
                });
            });
        }
    );
}

function pulseElement(element) {

    element.style.animation = '';
    void element.offsetWidth;       //important HACK to make the restart of animation work
    element.style.animation = "pulse .2s 1";
}