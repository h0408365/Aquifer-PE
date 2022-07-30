import axios from 'axios';
import * as helper from './serviceHelpers';

const selectbyUserLicenseId = (id) => {
    const config = {
        method: "GET"
        , url: `${helper.API_HOST_PREFIX}/api/verifylicense/${id}`
        , withCredentials: true
        , crossdomain: true
        , headers: { "Content-Type": "application/JSON" }
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
}

const update = (payload, id) => {
    const config = {
        method: "PUT"
        , data: payload
        , url: `${helper.API_HOST_PREFIX}/api/verifylicense/${id}`
        , withCredentials: true
        , crossdomain: true
        , headers: { "Content-Type": "application/JSON" }
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
}

export { selectbyUserLicenseId, update }