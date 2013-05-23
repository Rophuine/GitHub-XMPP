path = require('path');

module.exports = function (robot) {
    return robot.hear(/.*/i, function (msg) {
        return msg.send(path.join('test', 'really'));
    });
};