import React from "react";
import PropTypes from 'prop-types';
import debug from 'sabio-debug';
import 'rc-pagination/assets/index.css';
import './lvcardtest.css'
const _logger = debug.extend("ListingVerification")

function ListVerCard(props) {
    const aListing = props.listing;
    const onLocalLiVerClicked = (evt) => {
        evt.preventDefault();
        props.ListingClicker(props.listing, evt);
    }

    _logger(props, "props")

    return (
        <div className="col-md-3" >
            <div className="card" onClick={onLocalLiVerClicked}>
                {/* id={aListing.listing.id}  */}
                <div className="card-body">
                    <div className="col-md-4" style={{ width: '259px' }}>
                        <img className="card-img-top" src={aListing.listing.housingImages[0].url} alt="..." />
                    </div>
                    <div className="card-title">{aListing.listing.title}</div>
                    {/* <div className="card-subtitle">{aLiVer.url}</div> */}
                    <div className="card-text">{aListing.listing.shortDescription}</div>
                </div>
            </div>
        </div >

    )
}

ListVerCard.propTypes = {
    ListingClicker: PropTypes.func,
    listing: PropTypes.shape({
        listing: PropTypes.shape({
            id: PropTypes.number
            , title: PropTypes.string
            , shortDescription: PropTypes.string
            , description: PropTypes.string
            , housingImages: PropTypes.string
        })

    }),
}

export default ListVerCard;