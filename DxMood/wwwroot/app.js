const serviceURL = "http://localhost:5191";
const diagnoseButton = document.getElementById("diagnose");

let doctorID = "";

let doctorObject = {};

/**Doctor API**/

const createDoctor = () => {
    axios
        .post(`${serviceURL}/doctor`)
        .then((response) => {
            console.log(`POST doctor`, response);
        })
        .catch((error) => console.error(error));
};

//return patient list
const signInDoctor = (username, password) => {
    axios
        .post(`${serviceURL}/doctor/login`, [
            {
                username: username,
                password: password,
            },
        ])
        .then((response) => {
            console.log(`POST doctor login`, response);
            doctorObject = response;
            return response;
        })
        .catch((error) => console.error(error));
};

/**Patient API**/

const getPatients = (username, password) => {
    axios
        .get(`${serviceURL}/doctor/${username}/${password}`)
        .then((response) => {
            console.log(`GET patients`, response);
        })
        .catch((error) => console.error(error));
};

const createPatient = () => {
    axios
        .post(`${serviceURL}/patient`, [
            {
                "lastName": "string",
                "firstName": "string",
                "doctorID" : doctorObject.id,
            }
        ])
        .then((response) => {
            console.log(`PUT patient`, response);
        })
        .catch((error) => console.error(error));
};

/**Note API**/

/**Result API**/

diagnoseButton.onclick(() => {
    createPatient();
});

//getPatients();
