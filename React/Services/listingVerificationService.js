import axios from "axios";
import * as helper from "../../src/services/serviceHelpers"

const getByCreatedBy = (pageIndex, pageSize) => {
    const config = {
        method: "GET"
        , url: `${helper.API_HOST_PREFIX}/api/docverify/createdby/?pageIndex=${pageIndex}&pageSize=${pageSize}`
        , withCredentials: true
        , crossdomain: true
        , headers: { "Content-Type": "application/JSON" }
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

const getById = (id) => {
    const config = {
        method: "GET"
        , url: `${helper.API_HOST_PREFIX}/api/docverify/${id}`
        , withCredentials: true
        , crossdomain: true
        , headers: { "Content-Type": "application/JSON" }
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};


export { getByCreatedBy, getById }