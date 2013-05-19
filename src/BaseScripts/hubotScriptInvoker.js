var message;
var module = new Object();
var messages = new Array();

var bot = {
    hear: respondFunction,
    respond: respondFunction
};

function respondFunction(regex, callback) {
    if (regex.test(message.message)) {
        message.match = regex.exec(message.message);
        callback(message);
    }
}

function msgSend(message) {
    messages.push(message);
}

function pickRandom(items) {
    return items[Math.floor(Math.random() * items.length)];
}

// Creates a message object to be passed to a hubot responder.
// Check out Hubot github repo, /src/response.coffee

function MessageObject(room, user, message) {
    this.room = room;
    this.user = user;
    this.message = message;
    this.send = msgSend;
    this.random = pickRandom;
    this.http = function(a, b) {
        return new ScopedClient(a, b);
    };
}

