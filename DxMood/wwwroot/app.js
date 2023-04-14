const serviceURL = "http://localhost:5191";

//Create patient page
const diagnoseButton = document.getElementById("diagnose");
const inputName = document.getElementById("inp-name");
const inputDOB = document.getElementById("inp-dob");

//Patient List Page
const refreshPatientsButton = document.getElementById("refresh-patients-btn");
const patientDiv = document.getElementById("patient-list");

//Account Page
const doctorName = document.getElementById("doctor-name");
const numberPatients = document.getElementById("number-patients");

let doctorID = "";

let doctorObject = {};

let patientList = [];

/**Doctor API**/

const createDoctor = () => {
    let body = {
        id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        lastName: "string",
        firstName: "string",
        userName: "string",
        password: "string",
    };
    axios
        .post(`${serviceURL}/doctor`, body, {
            headers: {
                "Content-Type": "application/json",
            },
        })
        .then((response) => {
            console.log(`POST doctor`, response);
            doctorObject = response;
            console.log(doctorObject);
            doctorID = doctorObject?.id;
            patientList = doctorObject?.patients;
            updateDoctorName(doctorObject.lastname);
        })
        .catch((error) => console.error(error));
};

//return patient list
const signInDoctor = (username, password) => {
    let body = {
        username: username,
        password: password,
    };
    axios
        .post(`${serviceURL}/doctor/login`, body, {
            headers: {
                "Content-Type": "application/json",
            },
        })
        .then((response) => {
            console.log(`POST doctor login`, response);
            doctorObject = response;
            updateDoctorName(doctorObject.lastname);
            return response;
        })
        .catch((error) => console.error(error));
};

/**Patient API**/

const createPatient = (firstname, lastname, DOB, doctorID ) => {
    let body = {
        id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        lastName: lastname,
        firstName: firstname,
        dateOfBirth: DOB,
        doctorId: doctorID,
    };
    axios
        .post(`${serviceURL}/patient`, body, {
            headers: {
                "Content-Type": "application/json",
            },
        })
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
        createPatient("testfirst", "testlast", "1/23/2002", doctorID);
        //createPatient(inputName, inputName, inputDOB, doctorID);
    };
}

if (refreshPatientsButton) {
    refreshPatientsButton.onclick = () => {
        patientDiv.innerHTML = "";
        patientDiv.innerhtml = patientList;
        /*for(patient in patientList){
            patientDiv.innerHTML+=`<p>${patient}</p>`
        }*/
    };
}

const updateDoctorName = (lastname) => {
    doctorName.innerText = `Dr. ${lastname}'s profile`;
};

if (doctorName && doctorObject?.lastname) {
    updateDoctorName(doctorObject?.lastname);
} else {
    signInDoctor("username", "password");
}

createDoctor();

//getPatients();
