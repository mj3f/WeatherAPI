import http from 'k6/http';
import { sleep } from 'k6';
export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        { duration: '2m', target: 400 }, // quick ramp-up to 400 users
        { duration: '4h', target: 400 }, // stay at 400 for 4 hours
        { duration: '2m', target: 0 }, // scale down
    ]
};

export default () => {
    http.get('http://localhost:8080/api/v1/weather/london');
};