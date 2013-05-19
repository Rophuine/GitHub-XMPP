// compiled and modified based on https://github.com/technoweenie/node-scoped-http-client/blob/master/src/index.coffee
var ScopedClient, extend, http, https, path, qs, url;

//path = require('path');

//http = require('http');

//https = require('https');

//url = require('url');

//qs = require('querystring');

ScopedClient = (function () {

    function ScopedClient(url, options) {
        this.options = this.buildOptions(url, options);
    }

    ScopedClient.prototype.request = function (method, reqBody, callback) {
        var headers, port, req, sendingData,
      _this = this;
        if (typeof reqBody === 'function') {
            callback = reqBody;
            reqBody = null;
        }
        try {
            headers = extend({}, this.options.headers);
            sendingData = reqBody && reqBody.length > 0;
            headers.Host = this.options.hostname;
            if (reqBody != null) {
                headers['Content-Length'] = Buffer.byteLength(reqBody, this.options.encoding);
            }
            if (this.options.auth) {
                headers['Authorization'] = 'Basic ' + new Buffer(this.options.auth).toString('base64');
            }
            port = this.options.port || ScopedClient.defaultPort[this.options.protocol] || 80;
            req = (this.options.protocol === 'https:' ? https : http).request({
                port: port,
                host: this.options.hostname,
                method: method,
                path: this.fullPath(),
                headers: headers,
                agent: this.options.agent || false
            });
            if (callback) {
                req.on('error', callback);
            }
            if (sendingData) {
                req.write(reqBody, this.options.encoding);
            }
            if (callback) {
                callback(null, req);
            }
        } catch (err) {
            if (callback) {
                callback(err, req);
            }
        }
        return function (callback) {
            if (callback) {
                req.on('response', function (res) {
                    var body;
                    res.setEncoding(_this.options.encoding);
                    body = '';
                    res.on('data', function (chunk) {
                        return body += chunk;
                    });
                    return res.on('end', function () {
                        return callback(null, res, body);
                    });
                });
            }
            req.end();
            return _this;
        };
    };

    ScopedClient.prototype.fullPath = function (p) {
        var full, search;
        search = stringify(this.options.query);
        full = this.join(p);
        if (search.length > 0) {
            full += "?" + search;
        }
        return full;
    };

    ScopedClient.prototype.scope = function (url, options, callback) {
        var override, scoped;
        override = this.buildOptions(url, options);
        scoped = new ScopedClient(this.options).protocol(override.protocol).host(override.hostname).path(override.pathname);
        if (typeof url === 'function') {
            callback = url;
        } else if (typeof options === 'function') {
            callback = options;
        }
        if (callback) {
            callback(scoped);
        }
        return scoped;
    };

    ScopedClient.prototype.join = function (suffix) {
        var p;
        p = this.options.pathname || '/';
        if (suffix && suffix.length > 0) {
            if (suffix.match(/^\//)) {
                return suffix;
            } else {
                return pathjoin(p, suffix);
            }
        } else {
            return p;
        }
    };

    ScopedClient.prototype.path = function (p) {
        this.options.pathname = this.join(p);
        return this;
    };

    ScopedClient.prototype.query = function (key, value) {
        var _base;
        (_base = this.options).query || (_base.query = {});
        if (typeof key === 'string') {
            if (value) {
                this.options.query[key] = value;
            } else {
                delete this.options.query[key];
            }
        } else {
            extend(this.options.query, key);
        }
        return this;
    };

    ScopedClient.prototype.host = function (h) {
        if (h && h.length > 0) {
            this.options.hostname = h;
        }
        return this;
    };

    ScopedClient.prototype.port = function (p) {
        if (p && (typeof p === 'number' || p.length > 0)) {
            this.options.port = p;
        }
        return this;
    };

    ScopedClient.prototype.protocol = function (p) {
        if (p && p.length > 0) {
            this.options.protocol = p;
        }
        return this;
    };

    ScopedClient.prototype.encoding = function (e) {
        if (e == null) {
            e = 'utf-8';
        }
        this.options.encoding = e;
        return this;
    };

    ScopedClient.prototype.auth = function (user, pass) {
        if (!user) {
            this.options.auth = null;
        } else if (!pass && user.match(/:/)) {
            this.options.auth = user;
        } else {
            this.options.auth = "" + user + ":" + pass;
        }
        return this;
    };

    ScopedClient.prototype.header = function (name, value) {
        this.options.headers[name] = value;
        return this;
    };

    ScopedClient.prototype.headers = function (h) {
        extend(this.options.headers, h);
        return this;
    };

    ScopedClient.prototype.buildOptions = function () {
        var i, options, ty, _ref;
        options = {};
        i = 0;
        while (arguments[i]) {
            ty = typeof arguments[i];
            if (ty === 'string') {
                options.url = arguments[i];
            } else if (ty !== 'function') {
                extend(options, arguments[i]);
            }
            i += 1;
        }
        if (options.url) {
            extend(options, urlparse(options.url, true));
            delete options.url;
            delete options.href;
            delete options.search;
        }
        options.headers || (options.headers = {});
        if ((_ref = options.encoding) == null) {
            options.encoding = 'utf-8';
        }
        return options;
    };

    return ScopedClient;

})();

ScopedClient.methods = ["GET", "POST", "PATCH", "PUT", "DELETE", "HEAD"];

ScopedClient.methods.forEach(function (method) {
    return ScopedClient.prototype[method.toLowerCase()] = function (body, callback) {
        return this.request(method, body, callback);
    };
});

ScopedClient.prototype.del = ScopedClient.prototype['delete'];

ScopedClient.defaultPort = {
    'http:': 80,
    'https:': 443,
    http: 80,
    https: 443
};

extend = function (a, b) {
    var prop;
    prop = null;
    Object.keys(b).forEach(function (prop) {
        return a[prop] = b[prop];
    });
    return a;
};

pathjoin = function (a, b) {
    var end = b;
    while (end != '' && end.substring(0, 1) === '/') end = end.substring(1);
    var start = a;
    while (start != '' && start.substring(start.length - 2, start.length - 1) === '/') start = start.substring(0, start.length - 2);
    return start + '/' + end;
};

stringify = function (a) {
    var result = '';
    for (var key in a) {
        if (Object.prototype.toString.call(a[key]) === '[object Array]') {
            for (var i = 0; i < a[key].length; i++) {
                if (result == '') result += '?';
                else result += '&';
                result += encodeURIComponent(key) + '=' + encodeURIComponent(a[key][i]);
            }
        } else {
            if (result == '') result += '?';
            else result += '&';
            result += encodeURIComponent(key) + '=' + encodeURIComponent(a[key]);
        }
    }
    return result;
};
urlparse = function (url, ignored) {
    var regex = /(https?:)\/\/([^:@]*:[^@]*@)?([^:\/]+)(:\d+)?(\/[^\?#]+)(\?[^#]*)?(#.*)?/i;
    var groups = regex.exec(url);
    var result = new Object();
    result.href = url;
    if (groups[1] != null)
        result.protocol = groups[1];
    else
        result.protocol = 'http:';
    if (groups[2] != null) {
        var authlen = groups[2].length;
        result.auth = groups[2].substring(0, authlen - 1);
    } else
        result.auth = '';
    result.host = groups[3];
    result.hostname = groups[3];
    if (groups[4] != null) {
        result.port = groups[4].substring(1);
        result.host += groups[4];
    } else result.port = (result.protocol == 'https:' ? 443 : 80);
    if (groups[5] != null) {
        result.pathname = groups[5];
        result.path = groups[5];
    } else {
        result.path = '';
        result.pathname = '';
    }
    if (groups[6] != null) {
        result.path += groups[6];
        result.search = groups[6];
    } else result.search = '';
    if (groups[6] != null)
        result.query = groups[6].substring(1);
    else result.query = '';
    if (groups[7] != null)
        result.hash = groups[7];
    else result.hash = '';
    return result;
};
http = {
    request: function (options, callback) {
        return new HttpRequest(options, callback);
    }
};
https = {
    request: function (options, callback) {
        return new HttpRequest(options, callback);
    }
};