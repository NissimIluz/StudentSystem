const baseUrl = 'http://localhost:5187/api/';


function post(action, data) {
    try {
        return fetch(baseUrl + action, {
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        }).then(res => pipe(res))
        .catch(error => {
            return Promise.resolve(null);
        });
    }
    catch(error) {
        console.log(error);
        return Promise.resolve(null);
    }
}

function postFile(action, data) {
    try {
        return fetch(baseUrl + action, {
            method: "POST",
            body: data
        }).then(res => pipe(res) ).catch(error => {
            return Promise.resolve(null);
        });
        
    }
    catch(error) {
        console.log(error);
        return Promise.resolve(null);
    }
}

function pipe(res) {
    if (res.ok) {
        return res.json();
    }
    else {
        return Promise.resolve(null);
    }
}