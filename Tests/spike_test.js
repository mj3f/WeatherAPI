import http from 'k6/http';
import { sleep } from 'k6';
export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        { duration: '10s', target: 100 }, // below normal load
        { duration: '1m', target: 100 }, // 
        { duration: '10s', target: 1400 }, // spike of users
        // { duration: '1m', target: 1400 }, // stay at 1400 for a min 
        { duration: '10s', target: 200 }, // Scale down
        { duration: '2m', target: 100 }, // 
        { duration: '30s', target: 40 }, //
        { duration: '10s', target: 0 }, // 
    ]
};

const API_BASE_URL = 'http://localhost:8080/api/v1/weather'

export default () => {
    http.batch([
        ['GET', `${API_BASE_URL}/london`],
        ['GET', `${API_BASE_URL}/tokyo`],
        ['GET', `${API_BASE_URL}/chicago`],
    ])

};