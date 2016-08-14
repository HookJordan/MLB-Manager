﻿function searchPlayers(searchText) {

    // Clean the search text
    searchText = searchText.trim().toLowerCase();

    // Get a reference to the DOM element
    var e = document.querySelector('#playerList');

    // create an xhr object
    var xhr = new XMLHttpRequest();

    // configure its handler
    xhr.onreadystatechange = function () {

        if (xhr.readyState === 4) {
            // request-response cycle has been completed, so continue

            if (xhr.status === 200) {
                // request was successfully completed, and data was received, so continue

                // update the user interface
                e.innerHTML = xhr.responseText;

            } else {
                e.innerHTML = "<p>Request was not successful<br>(" + xhr.statusText + ")</p>";
            }
        } else {
            e.innerHTML = "<p>Loading...</p>";
        }
        // show the content
        e.style.display = 'block';
    }

    // configure the xhr object to fetch content
    xhr.open('get', '/Player/Players/' + searchText, true);
    // fetch the content
    xhr.send(null);
}

function searchCoaches(searchText) {

    // Clean the search text
    searchText = searchText.trim().toLowerCase();

    // Get a reference to the DOM element
    var e = document.querySelector('#coachList');

    // create an xhr object
    var xhr = new XMLHttpRequest();

    // configure its handler
    xhr.onreadystatechange = function () {

        if (xhr.readyState === 4) {
            // request-response cycle has been completed, so continue

            if (xhr.status === 200) {
                // request was successfully completed, and data was received, so continue

                // update the user interface
                e.innerHTML = xhr.responseText;

            } else {
                e.innerHTML = "<p>Request was not successful<br>(" + xhr.statusText + ")</p>";
            }
        } else {
            e.innerHTML = "<p>Loading...</p>";
        }
        // show the content
        e.style.display = 'block';
    }

    // configure the xhr object to fetch content
    xhr.open('get', '/Coach/Coaches/' + searchText, true);
    // fetch the content
    xhr.send(null);
}

function searchTeams(searchText) {

    // Clean the search text
    searchText = searchText.trim().toLowerCase();

    // Get a reference to the DOM element
    var e = document.querySelector('#teamList');

    // create an xhr object
    var xhr = new XMLHttpRequest();

    // configure its handler
    xhr.onreadystatechange = function () {

        if (xhr.readyState === 4) {
            // request-response cycle has been completed, so continue

            if (xhr.status === 200) {
                // request was successfully completed, and data was received, so continue

                // update the user interface
                e.innerHTML = xhr.responseText;

            } else {
                e.innerHTML = "<p>Request was not successful<br>(" + xhr.statusText + ")</p>";
            }
        } else {
            e.innerHTML = "<p>Loading...</p>";
        }
        // show the content
        e.style.display = 'block';
    }

    // configure the xhr object to fetch content
    xhr.open('get', '/Team/Teams/' + searchText, true);
    // fetch the content
    xhr.send(null);
}