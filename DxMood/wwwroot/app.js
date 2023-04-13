const serviceURL = "http://localhost:5191";

const diagnoseButton = document.getElementById("diagnose");
const refreshPatientsButton = document.getElementById("refresh-patients-btn");
const patientList = document.getElementById("patient-list");

let doctorID = "";

let doctorObject = {};

/**Doctor API**/

const createDoctor = () => {
    let data = {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "lastName": "string",
        "firstName": "string",
        "userName": "string",
        "password": "string"
      }
    axios
        .post(`${serviceURL}/doctor`, data, {
            headers: {
                "Content-Type": "application/json",
            },
        })
        .then((response) => {
            console.log(`POST doctor`, response);
            doctorObject = response;
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

const createPatient = () => {
    axios
        .post(`${serviceURL}/patient`, [
            {
                lastName: "string",
                firstName: "string",
                doctorID: doctorObject.id,
            },
        ])
        .then((response) => {
            console.log(`PUT patient`, response);
        })
        .catch((error) => console.error(error));
};

/**Note API**/

/**Result API**/

/** Function calls **/
if (diagnoseButton) {
    diagnoseButton.onclick = () => {
        createPatient();
    };
}

if (refreshPatientsButton) {
    refreshPatientsButton.onclick = () => {
        patientList.innerHTML = "";
        console.log("dog");
        let response = getPatients();
        patientList.innerhtml = response;
    };
}

createDoctor();

//getPatients();
